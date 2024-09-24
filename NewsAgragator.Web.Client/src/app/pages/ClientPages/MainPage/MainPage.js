import React, { useEffect, useLayoutEffect, useState } from 'react';
import { connect } from 'react-redux';
// import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../Wrapper/Wrapper';
import { loadData } from './actions';
import '../../ClientPages/ClientStyles.css';
import NewsShort from '../NewsPages/ClientNewsesPage/Components/NewsShort';



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
        props.loadData();
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

    const inlineStyle = {
        width: '40%',
        margin: '1rem auto',
        height: '10rem',
    }
    const renderComponent = () => {
        console.log("renderComponent");
        if (managingState.Loading == false) {
            return (
                <div className='mainComponent'>
                    <div className='mainComponentWeather'>
                        <div className='weatherShortBlock'>
                            WeatherComponent
                        </div>
                        <div className='weatherShortBlock'>
                            WeatherComponent
                        </div>
                        <div className='weatherShortBlock'> 
                            WeatherComponent
                        </div>
                    </div>
                    <div className='newsShortBlocks'>
                    {props.value.topNewses.map(x => (
                        // Переделать в отдельный компонент!! !!NewsShort!!
                        <div className='newsBlockShortContainer'>
                             <NewsShort 
                        
                            data = {x}
                            />
                        </div>  
                    ))}
                    </div>
                    <div className='btnLoadMore' onClick={() => loadMoreNewses()}> Load More Newses </div>
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
            <div className="clientMain">
                <div className="clientTitle">News Aggregator </div>
                {renderComponent()}
            </div>
        </Wrapper>
    );
};

const mapDispatchToProps = dispatch => {
    return {
        loadData: () => dispatch(loadData()),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.clientMain,
});

export default connect(mapStateToProps, mapDispatchToProps)(MainPage);