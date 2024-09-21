import React, { useEffect, useLayoutEffect, useState } from 'react';
import { connect } from 'react-redux';
// import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { load, loadParameters } from './actions';
import '../../../ClientPages/ClientStyles.css';


const MainPage = (props) => {

    const navigate = useNavigate();
    // const params = useParams();
    const [managingState, setValue] = useState({ Loading: true, });

    useLayoutEffect(() => {
        beforeRender();
    }, []);

    const beforeRender = () => {
        console.log("BeforeRender");    
        setValue({ AddOrChange: "Add", Loading: true });
        props.clearState();
        props.load();
    }

    useEffect(() => {
        console.log("propsLoading changed");
        // if (params.id != null
        //     && managingState.AddOrChange == "Change"
        //     && !props.value.loadingParameters
        //     && !props.value.loadingData) {
        //     title.onInitialize(props.value.title.value);
        //     setValue({ AddOrChange: "Change", Loading: props.value.loadingData });
        // }
        // else if (params.id == null
            
            if(!props.value.Loading)
            {
                setValue({ Loading: props.value.loading });
            }
    }, [props.value.loading]);
    
    const btnReadMore = (id) => {
        navigate('/ClientNewsFull/' + id);
    }

    const loadMoreNewses = () => {
        navigate("/ClientNewses");
    }

    const renderComponent = () => {
        console.log("renderComponent");
        if (managingState.Loading == false) {
            return (
                <div className='mainComponent'>
                    <div className='mainComponentWeather'>
                        <div className='weatherShortBlock'>
                            Картинка CommonWeather Today
                        </div>
                        <div className='weatherShortBlock'>
                            Картинка CommonWeather Tommorow
                        </div>
                        <div className='weatherShortBlock'> 
                        Картинка CommonWeather the day after Tommorow
                        </div>
                    </div>
                    <div className='newsShortBlocks'>
                    {props.value.topNews.value.map(x => (
                        // Переделать в отдельный компонент!! !!NewsShort!!
                        <div className='newsShort'>
                            <div className='newsShortTitle'>{x.title.value}</div>
                            <div className='newsShortDescription'>{x.description.value}</div>
                            <div className='btnReadMore' onClick={() => btnReadMore(x.id.value)}> Read Full </div>
                        </div>
                    ))}
                    </div>
                    <div className='btnLoadMore' onClick={() => loadMoreNewses(x.id.value)}> Load More Newses </div>
                </div>
            );
        }
        else if (managingState.Loading == true) {
            return
            (
                <div className="items loading">
                    ROCK
                    <FontAwesomeIcon icon={faSpinner} />
                </div>
            );
        }
    }

    console.log(props);
    return (

        <Wrapper>
            <div className="editPage">
                <div className="pageTitle">News Aggregator </div>
                {renderComponent()}



                <div className="btns">
                    <button className="btnAddChange" onClick={() => goToList()}>Back to List</button>
                    <div>
                        {addORchangeBtn()}
                    </div>
                </div>
            </div>
        </Wrapper>
    );
};

const mapDispatchToProps = dispatch => {
    return {
        load: () => dispatch(load()),
        clearState: (data) => dispatch(clearState(data)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.clientMain,
});

export default connect(mapStateToProps, mapDispatchToProps)(MainPage);