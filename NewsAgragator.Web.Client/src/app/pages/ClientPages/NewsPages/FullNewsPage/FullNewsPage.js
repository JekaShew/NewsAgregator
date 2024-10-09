import React, { useState, useEffect } from 'react';
// import HtmlParser from 'react-html-parser';
import { connect } from 'react-redux';
import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import ModalWrapper from '../Components/ModalWrapper';
import CreateComplaintPage from'../../ComplaintPages/CreateComplaintPage';
import ModalConfirmation from '../../../../customComponents/ModalConfirmation/ModalConfirmation';
import Comment from '../Components/Comment';
import { load, sendComment, select } from './actions';
import '../../../ClientPages/ClientStyles.css';

const useValidation = (value, validations) => {
    const [isEmpty, setEmpty] = useState({ value: true, errorMessage:"The field can't be Empty!" });
    const [minLengthError, setMinLengthError] = useState({ value: false, errorMessage: "" });
    const [maxLengthError, setMaxLengthError] = useState({ value: false, errorMessage: "" });
    const [emailError, setEmailError] = useState({ value: false, errorMessage: "The value is not Email!" });
    const [confirmationPasswordError, setConfirmationPasswordError] = useState({ value: false, errorMessage: "Password mismatch!" });
    const [inputValid, setInputValid] = useState(false);

    useEffect(() => {
        for (const validation in validations) {
            switch (validation) {
                case 'isEmpty':
                    value? setEmpty({value: false }) : setEmpty({value: true, errorMessage: "The field can't be Empty!"})
                    break;
                case 'minLength':
                    value.length < validations[validation]? 
                    setMinLengthError({value: true, errorMessage: "The value's length should be more than " + validations[validation] + "!"}) :
                    setMinLengthError({value: false}) ;
                    break;
                case 'maxLength':
                    value.length > validations[validation] ? 
                        setMaxLengthError({value: true, errorMessage: "The value's length should be less than " + validations[validation] + "!" }) :
                        setMaxLengthError({value: false});
                    break;
                case 'isEmail':
                    const re = /\S+@\S+\.\S+/;
                    re.test(String(value).toLowerCase()) ? 
                    setEmailError({value: false}) : 
                    setEmailError({value: true, errorMessage: "The value is not Email!"});
                    break;
                case 'confirmationPassword':
                    const pass = props.value.password.value;
                    value != pass ? 
                    setConfirmationPasswordError({value: true, errorMessage: "Password missmatch!"}) : 
                    setConfirmationPasswordError({value: false});
                    break;
            }
        }
    }, [value]);

    useEffect(() => {
        if (isEmpty.value || maxLengthError.value || minLengthError.value || emailError.value || confirmationPasswordError.value)
            setInputValid(false);
        else
            setInputValid(true);
            console.log(inputValid);
    }, [isEmpty, minLengthError, maxLengthError, emailError, confirmationPasswordError]);

    return {
        isEmpty,
        minLengthError,
        maxLengthError,
        emailError,
        confirmationPasswordError,
        inputValid
    }
}

const useInput = (validations) => {
    const [value, setValue] = useState("");
    const [isDirty, setDirty] = useState(false);
    const valid = useValidation(value, validations);

    const onChange = (e, select, inputTitle) => {
        setValue(e.target.value);
        select(inputTitle, e.target.value);

    }

    const onInitialize = (propValue) => {
        setValue(propValue);
    }

    const onBlur = (e) => {
        setDirty(true)
    }

    return {
        value,
        onInitialize,
        onChange,
        onBlur,
        isDirty,
        ...valid
    }
}

const useToken = () => {
    const [aToken, setAccessToken] = useState(JSON.parse(localStorage.getItem("AUTHORIZATION")).aToken || '');
    const [rToken, setRefreshToken] = useState(JSON.parse(localStorage.getItem("AUTHORIZATION")).rToken || '');
  
    const updateTokens = async (newAccessToken, newRefreshToken) => {
      setAccessToken(newAccessToken);
      setRefreshToken(newRefreshToken);
      localStorage.setItem("AUTHORIZATION", {aToken: newAccessToken, rToken :newRefreshToken});
    };
  
      const refreshAccessToken = async () => {
        console.log("RefreshToken")
          props.refresh();
        };
      
        useEffect(() => {
          const interval = setInterval(() => {
            if (aToken && rToken) {
              const expiresIn = new Date(accessToken.split('.')[1]).getTime();
              const currentTime = new Date().getTime();
              if (expiresIn - currentTime < 60000) {
                refreshAccessToken();
              }
            }
          }, 50000); 
      
          return () => clearInterval(interval);
        }, [aToken, rToken]);
      
        return { aToken, rToken, updateTokens, refreshAccessToken };
      };

const renderValidationMessages = (inputName) =>{
    if(inputName.isDirty){
        if(inputName.isEmpty.value)
            return  (<div style={{color:'red'}}>{inputName.isEmpty.errorMessage}</div>)
        else if(inputName.minLengthError.value) 
            return  (<div style={{color:'red'}}>{inputName.minLengthError.errorMessage}</div>)
        else if(inputName.maxLengthError.value) 
            return  (<div style={{color:'red'}}>{inputName.maxLengthError.errorMessage}</div>)
        else if(inputName.emailError.value) 
            return  (<div style={{color:'red'}}>{inputName.emailError.errorMessage}</div>)
        else if(inputName.confirmationPasswordError.value) 
            return  (<div style={{color:'red'}}>{inputName.confirmationPasswordError.errorMessage}</div>)
        else 
            return(<div style={{display:'block'}}></div>)
    }
}

const FullNewsPage = (props) => {

    // const [loading, setLoading] = useState(true);
    const [modalWrapper, setModalWrapper] = useState(
        { 
            ComplaintIds:
            {
                NewsId: '',
                CommentId: ''
            }, 
            ModalWrapperShow: false
        });
    
    const [isModalOpen, setIsModalOpen] = useState(false);
    const navigate = useNavigate();
    const [managingState, setValue] = useState({ Loading: true, });
    const params = useParams();
    const comment = useInput({isEmpty:true});
    // const { accessToken, refreshAccessToken } = useToken();
    const backToNewses = () => {
        navigate('/ClientNewses');
    }

    const bntNextNews = () => {
        let id = "next newsId";
        navigate('/ClientFullNews/' + id);
    }

    const btnSendComment = () =>{
        // let data = Object.fromEntries(Object.entries(props.value).map(e => [e[0], e[1].value]));

        console.log("DATA");
        // console.log(data);
        // let formData = new FormData();
        let comment = {
            accountId : null,
            text : props.value.comment.value,
            date: null,
            newsId: props.value.id.value,
        }
        console.log(comment);
        console.log(params.id);

        props.sendComment(comment,params.id);
    } 

    const openModal = (complaintIds) => {
        // setIsModalOpen(true);
        console.log("openModal");
        console.log(isModalOpen);
        <ModalWrapper isOpen={isModalOpen} onClose={closeModal}>
            <CreateComplaintPage
                 data = {complaintIds}/> 
        </ModalWrapper>
      };
    
      const closeModal = () => {
        setModalWrapper({ComplaintIds:{NewsId:'',CommentId:''},ModalWrapperShow:false});
      };

    const complaintToNews = (id) => {

    }

    const complaintToComment = (id) => {
        setModalWrapper({ ComplaintIds:{NewsId: props.value.id.value, CommentId: id}, ModalWrapperShow: true });
        setIsModalOpen(true);
        console.log("complaintToComment");
        
        console.log(props);

        let complaintIds = 
        {
            newsId: props.value.id.value,
            commentId: id,
        };
        console.log(complaintIds);
        
        openModal(complaintIds);
    };


    const beforeRender = () => {
        if (params.id != null) {
            setValue({  Loading: true });
            props.load(params.id);
        }
    }

    useEffect(() => {
        console.log("propsLoading changed");
        if (params.id != null
            && !props.value.loadingParameters
            && !props.value.loadingData) 
        {
            
            setValue({ Loading: props.value.loadingData });
        }
    }, [props.value.loadingParameters, props.value.loadingData]);

    useEffect(() => {
        beforeRender();
    }, []);

    const renderCommentInput = () =>{
        // let disabled = false;
        // if(!comment.inputValid)
        //     disabled = true;
        // else
        // disabled = false;   

            // <div className='fullNewsAddComment'>
            //     <div>
            //         <input
            //             className="input"
            //             type="text"
            //             placeholder="Your Comment"
            //             value={comment.value}
            //             onChange={(e) => comment.onChange(e,props.select,"comment")}
            //             onBlur={(e) => comment.onBlur(e)}
            //         />
            //         {
            //             renderValidationMessages(comment)
            //         }
            //     </div>

            //     <button 
            //         className="btnReadMore" 
            //         onClick={() => btnSubmitComment()}
            //     >
            //     Send Comment
            //     </button>
            // </div>
    }

    const renderComponent = () => {
        if (managingState.Loading == false) {
            return (
                <div>
                    <div className='fullNewsHead'>
                        <div className='fullNewsHeadSide '>
                            <button 
                                className="btnLoadMore" 
                                onClick={() => backToNewses()}
                            >
                                Back to Newses
                            </button>
                        </div>
                        <div className='fullNewsHeadCenter'>
                            <div className='fullNewsTitle'>
                                {props.value.title.value}
                            </div>
                            <div className='fullNewsHeadDateandRate'>
                                <div className='newsDate'>
                                    Date : {props.value.date.value}
                                </div>
                                <div className='newsRate'>Rate : 
                                    {props.value.positiveRating.value}
                                </div>
                            </div>
                        </div>
                        <div className='fullNewsHeadSide '>
                            <button 
                                className="btnLoadMore" 
                                onClick={() => bntNextNews()}
                            >
                                Next News
                            </button>
                        </div>
                    </div>
                    <div className='fullNewsTextHTML' dangerouslySetInnerHTML={{ __html: props.value.textHTML.value }}>
                       
                    </div>
                    <div className='complaintBtn' onClick={() => complaintToNews(props.value.id.value)}>
                        Complaint to News
                    </div>
                    <div className='fullNewsComments'>
                        {props.value.comments != null? props.value.comments.value.map(x => (
                            <div className='commentBlock'> 
                                <Comment 
                                    data = {x} 
                                />
                                <div className='commentbottomBlock'>
                                    <div>{x.date}</div>
                                <button 
                                    className="btnAddChange" 
                                    onClick={() => complaintToComment(x.id)}
                                >
                                    Complaint to News
                                </button>
                                </div>
                            </div>
                        ))
                        :
                        <div> No Comments yet</div>
                    }
                    </div>
                    <div className='fullNewsAddComment'>
                        <div className='inputWithError'>
                            <input
                                className="inputData"
                                type="text"
                                placeholder="Your Comment"
                                value={comment.value}
                                onChange={(e) => comment.onChange(e,props.select,"comment")}
                                onBlur={(e) => comment.onBlur(e)}
                            />
                            {
                                renderValidationMessages(comment)
                            }
                        </div>

                        <div 
                            className="btnReadMore" 
                            onClick={() => btnSendComment()}
                        >
                        Send Comment
                        </div>
                </div>    
            </div>
            )
        }
       
    }


    return (
        <Wrapper>
            <div className="clientFullNews">
                {renderComponent()}
                
            </div>
            <ModalWrapper isOpen={modalWrapper.ModalWrapperShow} onClose={closeModal}>
                <CreateComplaintPage
                     data = {modalWrapper.complaintIds}
                /> 
            </ModalWrapper>
        </Wrapper>
    );
};

const mapDispatchToProps = dispatch => {
    return {
        sendComment: (comment, id) => dispatch(sendComment(comment, id)),
        load: (id) => dispatch(load(id)),
        select: (name, value) => dispatch(select(name, value)),
        refresh: () => dispatch(refresh()),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.clientNewsFull,
});

export default connect(mapStateToProps, mapDispatchToProps)(FullNewsPage);