import React, { useEffect, useLayoutEffect, useState } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../Wrapper/Wrapper';
import { select, clearState, loadParameters, sendComplaint } from './actions';
import InputObject from '../../../customComponents/InputObject/InputObject';
import '../../ClientPages/ClientStyles.css';

const useValidation = (value, validations) => {
    const [isEmpty, setEmpty] = useState({ value: true, errorMessage:"The field can't be Empty!" });
    const [minLengthError, setMinLengthError] = useState({ value: false, errorMessage: "" });
    const [maxLengthError, setMaxLengthError] = useState({ value: false, errorMessage: "" });
    const [emailError, setEmailError] = useState({ value: false, errorMessage: "The value is not Email!" });
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
                        setEmailError({value: true, errorMessage: "The value is not Email!"}) : 
                        setEmailError({value: false});
                    break;
            }
        }
    }, [value]);

    useEffect(() => {
        if (isEmpty.value || maxLengthError.value || minLengthError.value || emailError.value)
            setInputValid(false);
        else
            setInputValid(true);
            console.log(inputValid);
    }, [isEmpty, minLengthError, maxLengthError, emailError]);

    return {
        isEmpty,
        minLengthError,
        maxLengthError,
        emailError,
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
        else 
            return(<div style={{display:'block'}}></div>)
    }
}

const CreateComplaintPage = (props) => {

    const navigate = useNavigate();
    const [managingState, setValue] = useState({Loading: true, });
    const title = useInput({isEmpty:true, minLength:2});
    const text = useInput({isEmpty:true, minLength:5});

    useLayoutEffect(() => {
        beforeRender();
    }, []);

    const beforeRender = () => {
        console.log("BeforeRender");
        console.log("CreateCompalintPage");
        setValue({Loading: true });
        props.clearState();
        props.loadParameters();
    }

    useEffect(() => {
       
        if (!props.value.loadingParameters) {
            title.onInitialize("");
            text.onInitialize("");
            setValue({ AddOrChange: "Add", Loading: props.value.loadingParameters });
        }
    }, [props.value.loadingParameters]);

    const createComplaintBtn = () => {
        let disabled = false;
        if(!title.inputValid && !text.inputValid)
            disabled = true;
        else
        disabled = false;

        if (managingState.AddOrChange == "Add")
            return (<button disabled={disabled} className="btnAddChange" onClick={() => sendComplaint()}>Add</button>);
    }

    const goToList = () => {
        navigate("/ClientFullNews/" + props.data.newsId);
    }

    const sendComplaint = () => {
        console.log("AddComplaintClient");
        console.log(props);
        let complaint ={
            newsId: props.data.NewsId,
            commentId: props.data.CommentId,
            title: title.value,
            text: text.value,
            complaintTypeId: props.complaintTypeId,
            userId: null,
        }
        console.log("COMPLAINT");
        console.log(complaint);
        props.sendComplaint(complaint);
    };

    const renderInputs = () => {
        console.log("renderInputsCreateComplaint");
        console.log(props);
        if (managingState.Loading == false) {
            return (
                <div className="editPageInputs">
                     <div className="divInput">
                        <div className="inputTitle">Complaint Type</div>
                        <InputObject
                            id="complaintType"
                            value={props.value.complaintType.value.id}
                            options={props.value.complaintTypes.value}
                            placeholder="Complaint Type"
                            onClick={(val, text) => props.select('complaintType', val, text)}
                        />
                    </div>

                    <div className="divInput">
                        <div className="inputTitle">Title</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Title"
                            value={title.value}
                            onChange={(e) => title.onChange(e,props.select,"title")}
                            onBlur={(e) => title.onBlur(e)}
                        />
                    </div>
                    {
                        renderValidationMessages(title)
                    }
                    <div className="divInput">
                        <div className="inputTitle">Text</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Text"
                            value={text.value}
                            onChange={(e) => text.onChange(e,props.select,"text")}
                            onBlur={(e) => text.onBlur(e)}
                        />
                    </div>
                    {
                        renderValidationMessages(text)
                    }

                   
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

    return (
            <div className="editPage">
                <div className="pageTitle">Send Complaint </div>
                {renderInputs()}



                <div className="btns">
                    <button className="btnAddChange" onClick={() => goToList()}>Back to List</button>
                    <div>
                        {createComplaintBtn()}
                    </div>
                </div>
            </div>
    );
};

const mapDispatchToProps = dispatch => {
    return {
        select: (name, value) => dispatch(select(name, value)),
        sendComplaint: (newComplaint) => dispatch(sendComplaint(newComplaint)),
        loadParameters: () => dispatch(loadParameters()),
        clearState: (data) => dispatch(clearState(data)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.createComplaint,
});

export default connect(mapStateToProps, mapDispatchToProps)(CreateComplaintPage);