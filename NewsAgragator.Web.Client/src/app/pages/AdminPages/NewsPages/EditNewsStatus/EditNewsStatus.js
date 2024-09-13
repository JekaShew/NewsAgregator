import React, { useEffect, useLayoutEffect, useState } from 'react';
import { connect } from 'react-redux';
import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { add, save, select, clearState, loadData } from './actions';
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

const EditNewsStatus = (props) => {

    const title = useInput({isEmpty:true, minLength:3});

    const navigate = useNavigate();
    const params = useParams();
    const [managingState, setValue] = useState({ AddOrChange: "", Loading: true, });

    useLayoutEffect(() => {
        beforeRender();
    }, []);

    const beforeRender = () => {
        console.log("BeforeRender");
        if (params.id != null) {
            setValue({ AddOrChange: "Change", Loading: true });
            props.loadData(params.id);

        }
        else {
            setValue({ AddOrChange: "Add", Loading: true });
            props.clearState();

        }
    }

    useEffect(() => {
        console.log("propsLoading changed");
        if (params.id != null
            && managingState.AddOrChange == "Change"
            && !props.value.loading) 
        {
            title.onInitialize(props.value.title.value);
            setValue({ AddOrChange: "Change", Loading: props.value.loading });
        }
        else if (params.id == null && !props.value.loading) 
        {
            title.onInitialize("");
            setValue({ AddOrChange: "Add", Loading: props.value.loading });
        }
    }, [props.value.loading]);

    const addORchangeBtn = () => {
        let disabled = false;
        if (!title.inputValid)
            disabled = true;
        else
            disabled = false;

        if (managingState.AddOrChange == "Add")
            return (<button disabled={disabled} className="btnAddChange" onClick={() => addNewsStatus()}>Add</button>);
        else if (managingState.AddOrChange == "Change")
            return (<button disabled={disabled} className="btnAddChange" onClick={() => changeNewsStatus()}>Change</button>);

    }

    const goToList = () => {
        navigate("/NewsStatuses");
    }

    const changeNewsStatus = () => {
        let data = Object.fromEntries(Object.entries(props.value).map(e => [e[0], e[1].value]));
        data.id = params.id;
        
        let formData = new FormData();
        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        props.save(formData);
    }

    const addNewsStatus = () => {
        let data = Object.fromEntries(Object.entries(props.value).map(e => [e[0], e[1].value]));
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
        console.log("renderInputs");

        if (managingState.Loading == false) {
            return (
                <div className="editPageInputs">
                    <div className="divInput">
                        <div className="inputTitle">Title</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Title"
                            value={title.value}
                            onChange={(e) => title.onChange(e, props.select, "title")}
                            onBlur={(e) => title.onBlur(e)}
                        />
                    </div>
                    {
                        renderValidationMessages(title)
                    }
                    <div className="divInput">
                        <div className="inputTitle">Description</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Description"
                            value={props.value.description.value}
                            onChange={(e) => props.select("description", e.target.value)}
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

    console.log(props);
    return ( 
        <Wrapper>
            <div className="editPage">
                <div className='pageTitle'> Edit News Status</div>
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
        add: (newNewsStatus) => dispatch(add(newNewsStatus)),
        save: (updatedNewsStatus) => dispatch(save(updatedNewsStatus)),
        clearState: (data) => dispatch(clearState(data)),
        loadData: (id) => dispatch(loadData(id)),
    }
 }

const mapStateToProps = (state) => ( console.log("mapStateToProps"),{
    
    value: state.editNewsStatus,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditNewsStatus);
