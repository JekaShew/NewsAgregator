import React, { useEffect, useLayoutEffect, useState } from 'react';
import { connect } from 'react-redux';
import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { add, save, select, selectParameter, clearState, load, loadParameters } from './actions';
import InputObject from '../../../../customComponents/InputObject/InputObject';
import '../../../AdminPages/EditPage.css';


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

const ClientEditAccountPage = (props) => {    

    const navigate = useNavigate();
    const params = useParams();
    const [managingState, setValue] = useState({ Loading: true, });
    const userName = useInput({isEmpty:true, minLength:3});
    const login = useInput({isEmpty:true, minLength:3});
    const password = useInput({isEmpty:true, minLength:5});
    const confirmationPassword = useInput({isEmpty:true, confirmationPassword:true});
    const secretWord = useInput({isEmpty:true, minLength:6});
    const fio = useInput({isEmpty:true});
    const email = useInput({isEmpty:true, isEmail:true});

    useLayoutEffect(() => {
        beforeRender();
    }, []);

    const beforeRender = () => {
        console.log("BeforeRender");
        if (params.id != null) {
            setValue({ Loading: true });
            props.load(params.id);
        } 
    }

    useEffect(() => {
        console.log("propsLoading changed");
        if (params.id != null
            && !props.value.loading) 
        {
            userName.onInitialize(props.value.userName.value);
            login.onInitialize(props.value.login.value);
            password.onInitialize(props.value.password.value);
            confirmationPassword.onInitialize(props.value.password.value);
            fio.onInitialize(props.value.fio.value);
            email.onInitialize(props.value.email.value);
            setValue({ Loading: props.value.loading });
        }
    }, [props.value.loading]);

    const renderSignUPbtns = () => {     
        let disabled = false;
        if(!userName.inputValid || !login.inputValid || !password.inputValid || !fio.inputValid || !email.inputValid || !confirmationPassword.inputValid)
            disabled = true;
        else
        disabled = false;
        return(
            <div>
                <button disabled={disabled} className="btnSign" onClick={() => changeAccount()}>Change Account</button>
                <button  className="btnSign" onClick={() => changePassword()}>Change Password</button>
            </div>
        );
    }

    const changePassword = () =>{
        navigate("/ChangePassword")
    }

    const changeAccount = () => {
        let data = Object.fromEntries(Object.entries(props.value).map(e => [e[0], e[1].value]));
        data.desiredNewsRating = Number.parseInt(data.desiredNewsRating);
        data.id = params.id;

        let formData = new FormData();
        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        props.save(formData);
    }

    const renderInputs = () => {        
        if (managingState.Loading == false) {
            return (
                <div className="editPageInputs">
                    <div className="divInput">
                        <div className="inputTitle">UserName</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="UserName"
                            value={userName.value}
                            onChange={(e) => userName.onChange(e,props.select,"userName")}
                            onBlur={(e) => userName.onBlur(e)}
                        />
                    </div>
                    {
                        renderValidationMessages(userName)
                    }
                    <div className="divInput">
                        <div className="inputTitle">FIO</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="FIO"
                            value={fio.value}
                            onChange={(e) => fio.onChange(e,props.select,"fio")}
                            onBlur={(e) => fio.onBlur(e)}
                        />
                    </div>
                    {
                        renderValidationMessages(fio)
                    }
                    <div className="divInput">
                        <div className="inputTitle">Email</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Email"
                            value={email.value}
                            onChange={(e) => email.onChange(e,props.select,"email")}
                            onBlur={(e) => email.onBlur(e)}
                        />
                    </div>
                    {
                        renderValidationMessages(email)
                    }
                    <div className="divInput">
                        <div className="inputTitle">Phone</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Phone"
                            value={props.value.phone.value}
                            onChange={(e) => props.select("phone", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">DesiredNewsRating</div>
                        <input
                            className="input"
                            type="number"
                            placeholder="Desired News Rating"
                            value={props.value.desiredNewsRating.value}
                            onChange={(e) => props.select("desiredNewsRating", e.target.value)}
                        />
                    </div>
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
                <div className=""> Client Edit Account </div>
                {renderInputs()}
                <div className="">
                    {addORchangeBtn()}
                </div>
            </div>
        </Wrapper>
    );
};

const mapDispatchToProps = dispatch => {
    return {
        select: (name, value) => dispatch(select(name, value)),
        save: (updatedAccount) => dispatch(save(updatedAccount)),
        load: (id) => dispatch(load(id)),
        clearState: () => dispatch(clearState()),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.clientEditAccount,
});

export default connect(mapStateToProps, mapDispatchToProps)(ClientEditAccountPage);