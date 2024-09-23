import React, { useEffect, useLayoutEffect, useState } from 'react';
import { connect } from 'react-redux';
// import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import {  select} from './actions';
import {signUP} from '../Authorization/actions';
// import InputObject from '../../../../customComponents/InputObject/InputObject';
import '../../../ClientPages/ClientStyles.css';


const useValidation = (value, validations) => {
    const [isEmpty, setEmpty] = useState({ value: true, errorMessage:"The field can't be Empty!" });
    const [minLengthError, setMinLengthError] = useState({ value: false, errorMessage: "" });
    const [maxLengthError, setMaxLengthError] = useState({ value: false, errorMessage: "" });
    const [emailError, setEmailError] = useState({ value: false, errorMessage: "The value is not Email!" });
    // const [confirmationPasswordError, setConfirmationPasswordError] = useState({ value: false, errorMessage: "Password mismatch!" });
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
                // case 'confirmationPassword':
                //     const pass = props != null? props.value.password.value : "";
                //     value != pass ? 
                //     setConfirmationPasswordError({value: true, errorMessage: "Password missmatch!"}) : 
                //     setConfirmationPasswordError({value: false});
                //     break;
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
        // else if(inputName.confirmationPasswordError.value) 
        //     return  (<div style={{color:'red'}}>{inputName.confirmationPasswordError.errorMessage}</div>)
        else 
            return(<div style={{display:'block'}}></div>)
    }
}

const SignUPPage = (props) => {    

    const navigate = useNavigate();
    // const params = useParams();
    const [managingState, setValue] = useState({ Loading: false });
    const userName = useInput({isEmpty:true, minLength:3});
    const login = useInput({isEmpty:true, minLength:3});
    const password = useInput({isEmpty:true, minLength:5});
    const confirmationPassword = useInput({isEmpty:true});
    const secretWord = useInput({isEmpty:true, minLength:6});
    const fio = useInput({isEmpty:true});
    const email = useInput({isEmpty:true, isEmail:true});
  
    useLayoutEffect(() => {
        beforeRender();
    }, []);

    const confirmationPasswordMessage = () =>{
        if(password.isDirty && confirmationPassword.isDirty)
        {
            if(password.value === confirmationPassword.value )
                return(<div style={{display:'block'}}></div>)
            else
                return (<div style={{color:'red'}}>Passwords missmatch!</div>);
        }   
    }

    useEffect(() => {
        confirmationPasswordMessage();
    }, [password.value, confirmationPassword.value]);

    const beforeRender = () => {
            setValue({ Loading: false });
        }

    useEffect(() => {
        console.log("propsLoading changed");
        if(!props.value.loading)
        {
            userName.onInitialize("");
            login.onInitialize("");
            password.onInitialize("");
            confirmationPassword.onInitialize("");
            fio.onInitialize("");
            email.onInitialize("");
            setValue({ Loading: props.value.loading });
        } 
                   
    }, [props.value.loading]);

    const signUP = () => {
        let data = Object.fromEntries(Object.entries(props.value).map(e => [e[0], e[1].value]));
        data.desiredNewsRating = Number.parseInt(data.desiredNewsRating);
        data.id = null;

        let formData = new FormData();
        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }
        console.log("signUP");
        console.log(data);
        console.log(formData);
        props.signUP(formData);
        
    };

    const renderSignUpBtn = () =>{
        let disabled = false;
        if(!userName.inputValid || !login.inputValid || !password.inputValid || !fio.inputValid || !email.inputValid || !confirmationPassword.inputValid || !secretWord.inputValid)
            disabled = true;
        else
        disabled = false;
        return (<div disabled={disabled} className="btnSign" onClick={() => signUP()}>Sign UP</div>);
    }

    const renderInputs = () => {        
        if (managingState.Loading == false) {
            return (
                <div className="signPageComponent">
                    <div className="inputBlock">
                        <div className="inputTitle">UserName</div>
                        <div className='inputWithError'>
                            <input
                                className="inputData"
                                type="text"
                                placeholder="UserName"
                                value={userName.value}
                                onChange={(e) => userName.onChange(e,props.select,"userName")}
                                onBlur={(e) => userName.onBlur(e)}
                            />
                            {
                                renderValidationMessages(userName)
                            }
                        </div>
                    </div>
                    
                    <div className="inputBlock">
                        <div className="inputTitle">FIO</div>
                        <div className='inputWithError'>
                            <input
                                className="inputData"
                                type="text"
                                placeholder="FIO"
                                value={fio.value}
                                onChange={(e) => fio.onChange(e,props.select,"fio")}
                                onBlur={(e) => fio.onBlur(e)}
                            />
                            {
                                renderValidationMessages(fio)
                            }
                            </div>
                    </div>
                    
                    <div className="inputBlock">
                        <div className="inputTitle">Email</div>
                        <div className='inputWithError'>
                            <input
                                className="inputData"
                                type="text"
                                placeholder="Email"
                                value={email.value}
                                onChange={(e) => email.onChange(e,props.select,"email")}
                                onBlur={(e) => email.onBlur(e)}
                            />
                            {
                                renderValidationMessages(email)
                            }
                        </div>
                    </div>
                    
                    <div className="inputBlock">
                        <div className="inputTitle">Phone</div>
                        <div className='inputWithError'>
                            <input
                                className="inputData"
                                type="text"
                                placeholder="Phone"
                                value={props.value.phone.value}
                                onChange={(e) => props.select("phone", e.target.value)}
                            />
                        </div>
                    </div>
                    <div className="inputBlock">
                        <div className="inputTitle">DesiredNewsRating</div>
                        <div className='inputWithError'>
                            <input
                                className="inputData"
                                type="number"
                                placeholder="Desired News Rating"
                                value={props.value.desiredNewsRating.value}
                                onChange={(e) => props.select("desiredNewsRating", e.target.value)}
                            />
                        </div>
                    </div>
                    <div className="inputBlock">
                        <div className="inputTitle">Login</div>
                        <div className='inputWithError'>
                            <input
                                className="inputData"
                                type="text"
                                placeholder="Login"
                                value={login.value}
                                onChange={(e) => login.onChange(e,props.select,"login")}
                                onBlur={(e) => login.onBlur(e)}
                            />
                            {
                                renderValidationMessages(login)
                            }   
                        </div>
                    </div>
                    
                    <div className="inputBlock">
                        <div className="inputTitle">Password</div>
                        <div className='inputWithError'>
                            <input
                                className="inputData"
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
                    </div>
                    
                    <div className="inputBlock">
                        <div className="inputTitle">Confirmation Password</div>
                        <div className='inputWithError'>
                            <input
                                className="inputData"
                                type="text"
                                placeholder="Confirmation Password"
                                value={confirmationPassword.value}
                                onChange={(e) => confirmationPassword.onChange(e,props.select,"confirmationPassword")}
                                onBlur={(e) => confirmationPassword.onBlur(e)}
                            />
                            {
                               confirmationPasswordMessage()
                            }
                        </div>
                    </div>

                    <div className="inputBlock">
                        <div className="inputTitle">Secret Word</div>
                        <div className='inputWithError'>
                        <input
                            className="inputData"
                            type="text"
                            placeholder="Secret Word"
                            value={secretWord.value}
                            onChange={(e) => secretWord.onChange(e,props.select,"secretWord")}
                            onBlur={(e) => secretWord.onBlur(e)}
                        />
                        {
                            renderValidationMessages(secretWord)
                        }
                        </div>
                    </div>
                    
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
            <div className="signUPPage">
                <div className="clientTitle">Signing UP </div>
                {renderInputs()}
                {renderSignUpBtn()}
            </div>
        </Wrapper>
    );
};

const mapDispatchToProps = dispatch => {
    return {
        select: (name, value) => dispatch(select(name, value)),     
        signUP: (newAccount) => dispatch(signUP(newAccount)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps",state), {

    value: state.signUP,
});

export default connect(mapStateToProps, mapDispatchToProps)(SignUPPage);