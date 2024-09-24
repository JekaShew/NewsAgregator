import React, { useEffect, useLayoutEffect, useState } from 'react';
import { connect } from 'react-redux';
// import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { loadData } from './actions';
import{ refresh } from '../../AccountPages/Authorization/actions'
import NewsShort from '../ClientNewsesPage/Components/NewsShort';
import '../../../ClientPages/ClientStyles.css';

const useToken = () => {
  const [aToken, setAccessToken] = useState(JSON.parse(localStorage.getItem("AUTHORIZATION")).aToken || '');
  const [rToken, setRefreshToken] = useState(JSON.parse(localStorage.getItem("AUTHORIZATION")).rToken || '');

  const updateTokens = async (newAccessToken, newRefreshToken) => {
    setAccessToken(newAccessToken);
    setRefreshToken(newRefreshToken);
    localStorage.setItem("AUTHORIZATION", {aToken: newAccessToken, rToken :newRefreshToken});
  };

    const refreshAccessToken = async () => {
        console.log("RefreshToken");
        props.refresh();
       
      };
    
      useEffect(() => {
        const interval = setInterval(() => {
          if (aToken && rToken) {
            const expiresIn = new Date(aToken.split('.')[1]).getTime();
            const currentTime = new Date().getTime();
            if (expiresIn - currentTime < 60000) {
              refreshAccessToken();
            }
          }
        }, 50000); 
    
        return () => clearInterval(interval);
      }, [aToken, rToken]);
    
      // Возвращаем объект с текущими токенами и функциями для их обновления
      return { aToken, rToken, updateTokens, refreshAccessToken };
    };


const ClientNewsesPage = (props) => {

    const navigate = useNavigate();
    // const params = useParams();
    const [managingState, setValue] = useState({ Loading: true, });
    const { accessToken, refreshAccessToken } = useToken();
    useLayoutEffect(() => {
        beforeRender();
    }, []);

    const beforeRender = () => {
        console.log("BeforeRender");    
        setValue({ Loading: true });
        props.loadData();
    }

    useEffect(() => {
        console.log("propsLoading changed");
        
            if(!props.value.Loading)
            {
                setValue({ Loading: props.value.loading });
            }
    }, [props.value.loading]);

    const renderComponent = () => {
        console.log("renderComponent");
        console.log("localStorage");
        console.log(JSON.parse(localStorage.getItem("AUTHORIZATION")));
        if (managingState.Loading == false) {
            return (
                <div className='newsShortBlocks'>
                {props.value.newses.map(x => (
                    <NewsShort 
                        key = {x.id}
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
            <div className="clientMain">
                <div className="clientTitle">Newses </div>
                {renderComponent()}
            </div>
        </Wrapper>
    );
};

const mapDispatchToProps = dispatch => {
    return {
        loadData: () => dispatch(loadData()),
        refresh: () => dispatch(refresh()),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.clientNews,
});

export default connect(mapStateToProps, mapDispatchToProps)(ClientNewsesPage);