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


const EditNews = (props) => {

    const text = useInput(props.value.editNews.text.value, {isEmpty:true, minLength:3, maxLength:3000});

    const navigate = useNavigate();
    const params = useParams();
    const [state, setValue] = useState({ AddOrChange: "", Loading: true, });

    const addORchangeBtn = () => {
        let disabled = false;
        if(!text.inputValid)
            disabled = true;
        else
        disabled = false;

        if (state.AddOrChange == "Add")
            return (<button disabled={disabled} className="btnAddChange" onClick={() => addNews()}>Add</button>);
        else if (state.AddOrChange == "Change")
            return (<button disabled={disabled} className="btnAddChange" onClick={() => changeNews()}>Change</button>);

    }

    const goToList = () => {
        navigate("/News");
    }

    const changeNews = () => {
        let data = Object.fromEntries(Object.entries(props.value.editNews).map(e => [e[0], e[1].value]));
        data.positiveRating = Number.parseInt(data.positiveRating);
        data.newsStatusId = data.newsStatus.id;
        data.id = params.id;

        let formData = new FormData();

        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }
        //formData.append('id', params.id);
        //formData.append('newsStatusId', props.value.editNews.newsStatus.value.id);

        props.save(formData);
    }

    const addNews = () => {
        let data = Object.fromEntries(Object.entries(props.value.editNews).map(e => [e[0], e[1].value]));
        data.positiveRating = Number.parseInt(data.positiveRating);
        data.newsStatusId = data.newsStatus.id;
        data.id = null;

        let formData = new FormData();

        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        //formData.append('newsStatusId', props.value.editNews.newsStatus.value.id);
        //formData.append('id', null);

        props.add(formData);
    };

    const renderInputs = () => {
        console.log("renderInputs");

        if (state.Loading == false) {

            return (
                <div className="editPageInputs">
                    <div className="divInput">
                        <div className="inputTitle">Title</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Title"
                            value={props.value.editNews.title.value}
                            onChange={(e) => props.select("title", e.target.value)}
                        />
                    </div>
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
                            value={props.value.editNews.date.value}
                            onChange={(e) => props.select("date", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Positive Rating</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Positive Rating"
                            value={props.value.editNews.positiveRating.value}
                            onChange={(e) => props.select("positiveRating", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">News Status</div>
                        <InputObject
                            id="newsStatus"
                            value={props.value.editNews.newsStatus.value.id}
                            options={props.value.editNews.newsStatuses.value}
                            placeholder="News Status"
                            onClick={(val, text) => props.selectParameter('newsStatus', val, text)}
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
            setValue({ AddOrChange: "Change", Loading: props.value.editNews.loading });
        }
        else if (params.id == null) {
            setValue({ AddOrChange: "Add", Loading: props.value.editNews.loading });
        }
        console.log(state.AddOrChange);
    }, [props.value.editNews.loading]);

    useLayoutEffect(() => {
        beforeRender();
    }, []);

    console.log(props);
    return (

        <Wrapper>
            <div className="editPage">
                <div className="pageTitle">Edit News </div>

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
        add: (newNews) => dispatch(add(newNews)),
        save: (updatedNews) => dispatch(save(updatedNews)),
        loadParameters: () => dispatch(loadParameters()),
        load: (id) => dispatch(load(id)),
        clearState: (data) => dispatch(clearState(data)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.editNews,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditNews);