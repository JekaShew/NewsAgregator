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

const EditAccount = (props) => {    

    const navigate = useNavigate();
    const params = useParams();
    const [managingState, setValue] = useState({ AddOrChange: "", Loading: true, });
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
        console.log("BeforeRender");
        if (params.id != null) {
            setValue({ AddOrChange: "Change", Loading: true });
            props.load(params.id);
        }
        else {
            setValue({ AddOrChange: "Add", Loading: true });
            props.clearState();
            props.loadParameters();
        }
    }

    useEffect(() => {
        console.log("propsLoading changed");
        console.log(props.value.loadingParameters);
        console.log(props.value.loadingData);
        if (params.id != null
            && managingState.AddOrChange == "Change"
            && !props.value.loadingParameters
            && !props.value.loadingData) 
        {
            userName.onInitialize(props.value.userName.value);
            login.onInitialize(props.value.login.value);
            password.onInitialize(props.value.password.value);
            confirmationPassword.onInitialize(props.value.password.value);
            fio.onInitialize(props.value.fio.value);
            email.onInitialize(props.value.email.value);
            setValue({ AddOrChange: "Change", Loading: props.value.loadingData });
        }
        else if (params.id == null
            && managingState.AddOrChange == "Add"
            && !props.value.loadingParameters) 
        {
            userName.onInitialize("");
            login.onInitialize("");
            password.onInitialize("");
            confirmationPassword.onInitialize("");
            fio.onInitialize("");
            email.onInitialize("");
            setValue({ AddOrChange: "Add", Loading: props.value.loadingParameters });
        }
    }, [props.value.loadingParameters, props.value.loadingData]);

    const addORchangeBtn = () => {     
        let disabled = false;
        if(!userName.inputValid || !login.inputValid || !password.inputValid || !fio.inputValid || !email.inputValid)
            disabled = true;
        else
        disabled = false;

        if (managingState.AddOrChange == "Add")
            return (<button disabled={disabled} className="btnAddChange" onClick={() => addAccount()}>Add</button>);
        else if (managingState.AddOrChange == "Change")
            return (<button disabled={disabled} className="btnAddChange" onClick={() => changeAccount()}>Change</button>);
    }

    const goToList = () => {
        navigate("/Accounts");
    }

    const changeAccount = () => {
        let data = Object.fromEntries(Object.entries(props.value).map(e => [e[0], e[1].value]));
        data.desiredNewsRating = Number.parseInt(data.desiredNewsRating);
        data.accountStatusId = data.accountStatus.id;
        data.roleId = data.role.id;
        data.id = params.id;

        let formData = new FormData();
        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        props.save(formData);
    }

    const addAccount = () => {
        let data = Object.fromEntries(Object.entries(props.value).map(e => [e[0], e[1].value]));
        data.desiredNewsRating = Number.parseInt(data.desiredNewsRating);
        data.accountStatusId = data.accountStatus.id;
        data.roleId = data.role.id;
        data.id = null;

        let formData = new FormData();
        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        props.add(formData);
    };

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
                    <div className="divInput">
                        <div className="inputTitle">Password</div>
                        <input
                            className="input"
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
                    <div className="divInput">
                        <div className="inputTitle">Confirmation Password</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Confirmation Password"
                            value={confirmationPassword.value}
                            onChange={(e) => confirmationPassword.onChange(e,props.select,"confirmationPassword")}
                            onBlur={(e) => confirmationPassword.onBlur(e)}
                        />
                    </div>
                    {
                        confirmationPasswordMessage()
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
                        <div className="inputTitle">Account Status</div>
                        <InputObject
                            id="accountStatuses"
                            value={props.value.accountStatus.value.id}
                            options={props.value.accountStatuses.value}
                            placeholder="Account Status"
                            onClick={(val, text) => props.selectParameter('accountStatus', val,text)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Role</div>
                        <InputObject
                            id="roles"
                            value={props.value.role.value.id}
                            options={props.value.roles.value}
                            placeholder="Role"
                            onClick={(val,text) => props.selectParameter('role', val, text)}
                        />
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
            <div className="editPage">
                <div className="pageTitle">Edit Account </div>
                {renderInputs()}
                <div className="btns">
                    <button className="btnAddChange" onClick={() => goToList()}>Back to List</button>
                    <div>
                        {addORchangeBtn()}
                    </div>
                </div>
            </div>
        </Wrapper>
    );
};

const mapDispatchToProps = dispatch => {
    return {
        select: (name, value) => dispatch(select(name, value)),
        selectParameter: (name, value, text) => dispatch(selectParameter(name, value, text)),
        add: (newAccount) => dispatch(add(newAccount)),
        save: (updatedAccount) => dispatch(save(updatedAccount)),
        loadParameters: () => dispatch(loadParameters()),
        load: (id) => dispatch(load(id)),
        clearState: (data) => dispatch(clearState(data)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.editAccount,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditAccount);