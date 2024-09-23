import React, { useEffect, useLayoutEffect, useState } from 'react';
import { connect } from 'react-redux';
// import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { checkLoginAndSecretWord, select, clearState } from './actions';
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

const ForgotPasswordPage = (props) => {    

    const navigate = useNavigate();
    // const params = useParams();
    const login = useInput({isEmpty:true});
    const secretWord = useInput({isEmpty:true});
    const newPassword = useInput({isEmpty:true, minLength:5});
    const confirmationNewPassword = useInput({isEmpty:true, confirmationPassword:true});

    useLayoutEffect(() => {
        beforeRender();
    }, []);

    const beforeRender = () => {
        console.log("BeforeRender");

        props.clearState();
    }

    const changePasswordWhenForgot = () =>{
        let data = Object.fromEntries(Object.entries(props.value).map(e => [e[0], e[1].value]));

        let formData = new FormData();
        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        props.checkLoginAndSecretWord(formData);
    }

    const renderForgotBtn = () => {     
        let disabled = false;
        if(!login.inputValid || secretWord.inputValid)
            disabled = true;
        else
        disabled = false;

        return (<button disabled={disabled} className="btnSign" onClick={() => changePasswordWhenForgot()}>Change Password</button>);
    }

    const renderInputs = () => {        
        if (managingState.Loading == false) {
            return (
                <div className="editPageInputs">
                    
                    <div className="divInput">
                        <div className="inputTitle">Login</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Login"
                            value={login.value}
                            onChange={(e) => login.onChange(e,props.select,"login")}
                            onBlur={(e) => login.onBlur(e)}
                        />
                    </div>
                    {
                        renderValidationMessages(login)
                    } 

                    <div className="divInput">
                        <div className="inputTitle">Secret Word</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Secret Word"
                            value={secretWord.value}
                            onChange={(e) => secretWord.onChange(e,props.select,"secretWord")}
                            onBlur={(e) => secretWord.onBlur(e)}
                        />
                    </div>
                    {
                        renderValidationMessages(secretWord)
                    }
                     <div className="divInput">
                        <div className="inputTitle">New Password</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="New Password"
                            value={newPassword.value}
                            onChange={(e) => newPassword.onChange(e,props.select,"newPassword")}
                            onBlur={(e) => newPassword.onBlur(e)}
                        />
                    </div>
                    {
                        renderValidationMessages(newPassword)
                    }
                    <div className="divInput">
                        <div className="inputTitle">Confirmation New Password</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Confirmation New Password"
                            value={confirmationNewPassword.value}
                            onChange={(e) => confirmationNewPassword.onChange(e,props.select,"confirmationNewPassword")}
                            onBlur={(e) => confirmationNewPassword.onBlur(e)}
                        />
                    </div>
                    {
                        renderValidationMessages(confirmationNewPassword)
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

        <Wrapper>
            <div className="">
                <div className="">Change Password When Forgot </div>
                {renderInputs()}
                <div className="">
                    {renderForgotBtn()}
                </div>
            </div>
        </Wrapper>
    );
};

const mapDispatchToProps = dispatch => {
    return {
        select: (name, value) => dispatch(select(name, value)),
        changePasswordWhenForgot: (someData) => dispatch(changePasswordWhenForgot(someData)),
        clearState: (data) => dispatch(clearState(data)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.changePasswordWhenForgot,
});

export default connect(mapStateToProps, mapDispatchToProps)(ForgotPasswordPage);