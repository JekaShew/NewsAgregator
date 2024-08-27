import React, { useEffect, useLayoutEffect, useState } from 'react';
import { connect } from 'react-redux';
import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { add, save, select, selectParameter, clearState, load, loadParameters } from './actions'; 
import '../../../AdminPages/EditPage.css';


const useValidation = (value, validations) => {
    const [isEmpty, setEmpty] = useState({ value: true, errorMessage:"The field can't be Empty!" });
    const [minLengthError, setMinLengthError] = useState({ value: false, errorMessage: "The value's length should be more than " });
    const [maxLengthError, setMaxLengthError] = useState({ value: false, errorMessage: "The value's length should be less than " });
    const [emailError, setEmailError] = useState({ value: false, errorMessage: "The value is not Email!" });
    const [inputValid, setInputValid] = useState(false);


    useEffect(() => {
        for (const validation in validations) {
            switch (validation) {
                case 'isEmpty':
                    value? setEmpty({value: false }) : setEmpty({value: true, errorMessage: isEmpty.errorMessage})
                    break;
                case 'minLength':
                    value.length < validations[validation] ? 
                        setMinLengthError({value: true, errorMessage: minLengthError.errorMessage + validations[validation] + "!"}) :
                        setMinLengthError({value: false});
                    break;
                case 'maxLength':
                    value.length > validations[validation] ? 
                        setMaxLengthError({value: true, errorMessage: maxLengthError.errorMessage + validations[validation] + "!"}) :
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
        if (isEmpty || maxLengthError || minLemgthError || emailError)
            setInputValid(false);
        else
            setInputValid(true);
    }, [isEmpty, minLengthError, maxLengthError, emailError]);

    return {
        isEmpty,
        minLengthError,
        maxLengthError,
        emailError,
        inputValid
    }
}

const useInput = (initialValue,validations ) => {

    const [value, setValue] = useState(initialValue);
    const [isDirty, setDirty] = useState(false);
    const valid = useValidation(value, validations);  

    const onChange = (e) => {
        setValue(e.target.value);

    }

    const onBlur = (e) => {
        setDirty(true)
    }


    return {
        value,
        onChange,
        onBlur,
        isDirty,
        ...valid
    }
}
// todo improve React validation (error Messages, disabled button, etc)

const ExtendedInput = (props) => {

    const userName = useInput(props.value.editAccount.userName.value, {isEmpty:true, minLength:3});

return (

    <div className="divInput">
        <div className="inputTitle">UserName</div>
        <input
            className="input"
            type="text"
            placeholder="UserName"
            value={userName.value}
            onChange={(e) => { userName.onChange(e); props.select("userName", e.target.value)}}
            onBlur={(e) => userName.onBlur(e)}
        />
    </div>
    {
            <div style={{color:'red'}}>{userName.isEmpty.errorMessage}</div> 
        else if(userName.minLengthError)
            <div style={{color:'red'}}>{userName.minLengthError.errorMessage}</div>
        else
        <div style={{display: 'none'}}></div>
    }
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

export default connect(mapStateToProps, mapDispatchToProps)(ExtendedInput);