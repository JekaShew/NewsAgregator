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

const useInput = (initialValue,validations ) => {
    const [value, setValue] = useState(initialValue);
    const [isDirty, setDirty] = useState(false);
    const valid = useValidation(value, validations);  

    const onChange = (e,select,inputTitle) => {
        setValue(e.target.value);
        select(inputTitle, e.target.value);

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

const EditComment = (props) => {

    const text = useInput(props.value.editComment.text.value, {isEmpty:true});
    const date = useInput(props.value.editComment.date.value, {isEmpty:true});

    const navigate = useNavigate();
    const params = useParams();
    const [state, setValue] = useState({ AddOrChange: "", Loading: true, });

    const addORchangeBtn = () => {
        let disabled = false;
        if(!text.inputValid && !date.inputValid)
            disabled = true;
        else
        disabled = false;

        if (state.AddOrChange == "Add")
            return (<button disabled={disabled} className="btnAddChange" onClick={() => addComment()}>Add</button>);
        else if (state.AddOrChange == "Change")
            return (<button disabled={disabled} className="btnAddChange" onClick={() => changeComment()}>Change</button>);

    }

    const goToList = () => {
        navigate("/Comments");
    }

    const changeComment = () => {
        let data = Object.fromEntries(Object.entries(props.value.editComment).map(e => [e[0], e[1].value]));
        data.newsId = data.news.id;
        data.accountId = data.account.id;
        data.id = params.id;


        let formData = new FormData();

        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        props.save(formData);
    }

    const addComment = () => {
        let data = Object.fromEntries(Object.entries(props.value.editComment).map(e => [e[0], e[1].value]));
        data.newsId = data.news.id;
        data.accountId = data.account.id;
        data.id = null;
        console.log("AddComment");
        console.log(data)
        let formData = new FormData();

        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        props.add(formData);
    };

    const renderInputs = () => {
        console.log("renderInputs");

        if (state.Loading == false) {
            return (
                <div className="editPageInputs">
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
                    <div className="divInput">
                        <div className="inputTitle">Date</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Date"
                            value={date.value}
                            onChange={(e) => date.onChange(e,props.select,"date")}
                            onBlur={(e) => date.onBlur(e)}
                        />
                    </div>
                    {
                        renderValidationMessages(date)
                    }
                    <div className="divInput">
                        <div className="inputTitle">Account </div>
                        <InputObject
                            id="account"
                            value={props.value.editComment.account.value.id}
                            options={props.value.editComment.accounts.value}
                            placeholder="Account"
                            onClick={(val, text) => props.selectParameter('account', val, text)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">News</div>
                        <InputObject
                            id="news"
                            value={props.value.editComment.news.value.id}
                            options={props.value.editComment.newses.value}
                            placeholder="News"
                            onClick={(val, text) => props.selectParameter('news', val, text)}
                        />
                    </div>
                </div>
                 );
        }
        else if (state.Loading == true) {
            return
            (
                <div className="items loading">
                    ROCK
                    <FontAwesomeIcon icon={faSpinner} />
                </div>
            );
        }
    }

    const beforeRender = () => {
        console.log("BeforeRender");
        if (params.id != null) {
            setValue({ AddOrChange: "Change", Loading: true });
            props.load(params.id);
            console.log(params.id);
            console.log(state.AddOrChange);

        }
        else {
            setValue({ AddOrChange: "Add", Loading: true });
            props.clearState();
            props.loadParameters();
            console.log(state.AddOrChange);

        }
    }

    useEffect(() => {
        console.log("propsLoading changed");
        if (params.id != null) {
            setValue({ AddOrChange: "Change", Loading: props.value.editComment.loading });
        }
        else if (params.id == null) {
            setValue({ AddOrChange: "Add", Loading: props.value.editComment.loading });
        }
        console.log(state.AddOrChange);
    }, [props.value.editComment.loading]);

    useLayoutEffect(() => {
        beforeRender();
    }, []);

    console.log(props);
    return (

        <Wrapper>
            <div className="editPage">
                <div className="pageTitle">Edit Comment </div>
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
        add: (newComment) => dispatch(add(newComment)),
        save: (updatedComment) => dispatch(save(updatedComment)),
        loadParameters: () => dispatch(loadParameters()),
        load: (id) => dispatch(load(id)),
        clearState: (data) => dispatch(clearState(data)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.editComment,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditComment);