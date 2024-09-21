import React, { useEffect, useLayoutEffect, useState } from 'react';
import { connect } from 'react-redux';
// import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { signIN, select, clearState } from './actions';

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

const SignINPage = (props) => {    

    const navigate = useNavigate();
    // const params = useParams();
    const [managingState, setValue] = useState({ Loading: true, });
    const login = useInput({isEmpty:true, minLength:3});
    const password = useInput({isEmpty:true, minLength:5});

    useLayoutEffect(() => {
        beforeRender();
    }, []);

    const beforeRender = () => {
        console.log("BeforeRender");    
        setValue({ Loading: true });
        props.clearState();
    }

    useEffect(() => {
        console.log("propsLoading changed");
        
        if(!props.value.loading)
        {
            login.onInitialize(props.value.login.value);
            password.onInitialize(props.value.password.value);

            setValue({ Loading: props.value.loading });
        }
    }, [props.value.loading]);

    const renderSignINPageBtns = () => {     
        let disabled = false;
        if(!login.inputValid || !password.inputValid)
            disabled = true;
        else
        disabled = false;
            return (
            <div>
                <button disabled={disabled} className="btnSign" onClick={() => signIN()}>Sign IN</button>
                <div>
                New User?
                <button className="btnMin" onClick={() => toSignUPPage()}>Sign UP!</button>
                </div>
            </div>
            );
    }

    const toForgotPassword = () => {
        navigate("/ForgotPassword");
    }

    const toSignUPPage = () => {
        navigate("/SignUP");
    }

    const signIN = () => {
        let data = Object.fromEntries(Object.entries(props.value).map(e => [e[0], e[1].value]));

        let formData = new FormData();
        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        props.signIN(formData);
    };

    const renderComponent = () => {        
        if (managingState.Loading == false) {
            return (
                <div className="">
                    <div className="">
                        <div className="">Login</div>
                        <input
                            className=""
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
                    <div className="">
                        <div className="">Password</div>
                        <input
                            className=""
                            type="text"
                            placeholder="Password"
                            value={password.value}
                            onChange={(e) => password.onChange(e,props.select,"password")}
                            onBlur={(e) => password.onBlur(e)}
                        />
                    </div>
                    {
                        renderValidationMessages(password)
                    }
                    <button disabled={disabled} className="btnMin" onClick={() => toForgotPassword()}>Forgot your password?</button>
                   
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
                <div className="">Signing IN </div>
                {renderComponent()}
                <div>
                    {renderSignINPageBtns()}
                </div>
            </div>
        </Wrapper>
    );
};

const mapDispatchToProps = dispatch => {
    return {
        select: (name, value) => dispatch(select(name, value)), 
        signIN: (login, password) => dispatch(signIN(login, password)),
        clearState: () => dispatch(clearState()),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.signIN,
});

export default connect(mapStateToProps, mapDispatchToProps)(SignINPage);