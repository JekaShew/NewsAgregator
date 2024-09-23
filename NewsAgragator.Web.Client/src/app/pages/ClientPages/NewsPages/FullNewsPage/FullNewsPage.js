import React, { useState, useEffect } from 'react';
// import HtmlParser from 'react-html-parser';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import ModalConfirmation from '../../../../customComponents/ModalConfirmation/ModalConfirmation';
import { loadData } from './actions';
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

    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();
    const comment = useInput({isEmpty:true});

    const backToNewses = () => {
        navigate('/ClientNewses');
    }

    const bntNextNews = () => {
        let id = "next newsId";
        navigate('/ClientFullNews/' + id);
    }

    const btnSubmitComment = () =>{

    }

    const renderSubmitBtn = () => {
        let disabled = false;
        if(!comment.inputValid)
            disabled = true;
        else
        disabled = false;
       
        return 
        (
            <button 
            className="btn" 
            onClick={() => btnSubmitComment()}
        >
            Send Comment
        </button>
        );
        
    }

    const renderComponent = () => {
        if (loading == false) {
            return (
                <div>
                    <div className='fullNewsHead'>
                        <div className='fullNewsHeadSide '>
                            <button 
                                className="btnAddChange" 
                                onClick={() => backToNewses()}
                            >
                                Back to list of Newses
                            </button>
                        </div>
                        <div className='fullNewsHeadCenter'>
                            <div className='fullNewsTitle'>
                                {props.value.title.value}
                            </div>
                            <div className='fullNewsHeadDateandRate'>
                                <div className='newsDate'>
                                    {props.value.date.value}
                                </div>
                                <div className='newsRate'>
                                    {props.value.positriveRating.value}
                                </div>
                            </div>
                        </div>
                        <div className='fullNewsHeadSide '>
                            <button 
                                className="btnAddChange" 
                                onClick={() => bntNextNews()}
                            >
                                Next News
                            </button>
                        </div>
                    </div>
                    <div className='fullNewsTextHTML' dangerouslySetInnerHTML={{ __html: props.value.textHTML }}>
                        {/* {HtmlParser(props.value.textHTML)} */}
                    </div>
                    <div className='complaintBtn'>
                    <button 
                        className="btnAddChange" 
                        onClick={() => complaintToNews()}
                    >
                        Complaint to News
                    </button>
                    </div>
                    <div className='fullNewsComments'>
                        {props.value.fullNews.comments.map(x => (
                            <div> 
                                <Comment 
                                    data = {x} 

                                />
                                <button 
                                    className="btnAddChange" 
                                    onClick={() => complaintToComment()}
                                >
                                    Complaint to News
                                </button>
                            </div>
                        ))}
                    </div>
                    <div className='fullNewsAddComment'>
                        <div>
                            <input
                                className="input"
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

                        {renderSubmitBtn()}
                    </div>
                            
                </div>
            )
        }
        else
            return
        (<div className="items loading">
            <FontAwesomeIcon icon={faSpinner} />
        </div>)
    }

    const beforeRender = () => {
        console.log("BeforeRender");
        setLoading(true);
        props.loadData();
    }

    useEffect(() => {
        setLoading(props.value.fullNews.loading);
    }, [props.value.fullNews.loading]);

    useEffect(() => {
        beforeRender();
    }, []);


    return (
        <Wrapper>
            <div className="editPage">
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

    value: state.fullNews,
});

export default connect(mapStateToProps, mapDispatchToProps)(FullNewsPage);