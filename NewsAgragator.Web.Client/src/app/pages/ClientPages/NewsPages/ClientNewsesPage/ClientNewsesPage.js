import React, { useEffect, useLayoutEffect, useState } from 'react';
import { connect } from 'react-redux';
// import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { load } from './actions';
import NewsShort from '../ClientNewsesPage/Components/NewsShort';
import '../../../ClientPages/ClientStyles.css';


const ClientNewsesPage = (props) => {

    const navigate = useNavigate();
    // const params = useParams();
    const [managingState, setValue] = useState({ Loading: true, });

    useLayoutEffect(() => {
        beforeRender();
    }, []);

    const beforeRender = () => {
        console.log("BeforeRender");    
        setValue({ Loading: true });
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

    const renderComponent = () => {
        console.log("renderComponent");
        if (managingState.Loading == false) {
            return (
                <div className='newsShortBlocks'>
                {props.value.topNews.value.map(x => (
                    <NewsShort 
                        data = {x}
                    />      
                ))}
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
                <div className="pageTitle">Newses </div>
                {renderComponent()}
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

    value: state.clientNewses,
});

export default connect(mapStateToProps, mapDispatchToProps)(ClientNewsesPage);