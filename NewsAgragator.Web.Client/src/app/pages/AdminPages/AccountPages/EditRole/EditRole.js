import React, { useEffect, useLayoutEffect, useState } from 'react';
import { connect } from 'react-redux';
import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import InputMultyplyObject from '../../../../customComponents/InputMultiplyObject/InputMultyplyObject';
import { add, save, select, clearState, load,loadParameters, addList, removeList } from './actions';
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

const EditRole = (props) => {

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
        if (params.id != null
            && managingState.AddOrChange == "Change"
            && !props.value.loadingParameters
            && !props.value.loadingData) 
        {
            title.onInitialize(props.value.title.value);
            setValue({ AddOrChange: "Change", Loading: props.value.loadingData });
        }
        else if (params.id == null
            && managingState.AddOrChange == "Add"
            && !props.value.loadingParameters) 
        {
            title.onInitialize("");
            setValue({ AddOrChange: "Add", Loading: props.value.loadingParameters });
        }
    }, [props.value.loadingParameters, props.value.loadingData]);

    const addORchangeBtn = () => {
        let disabled = false;
        if (!title.inputValid)
            disabled = true;
        else
            disabled = false;

        if (managingState.AddOrChange == "Add")
            return (<button disabled={disabled} className="btnAddChange" onClick={() => addRole()}>Add</button>);
        else if (managingState.AddOrChange == "Change")
            return (<button disabled={disabled} className="btnAddChange" onClick={() => changeRole()}>Change</button>);

    }

    const goToList = () => {
        navigate("/Roles");
    }

    const changeRole = () => {
        let data = Object.fromEntries(Object.entries(props.value).map(e => [e[0], e[1].value]));
        data.id = params.id;
        
        let formData = new FormData();
        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        formData.delete('policies');
        for (var i = 0; i < data.policies.length; i++)
            formData.append('policiesIDs', data.policies[i]);

        props.save(formData);
    }

    const addRole = () => {
        let data = Object.fromEntries(Object.entries(props.value).map(e => [e[0], e[1].value]));
        data.id = null;

        let formData = new FormData();
        console.log("addRole");
        console.log(data);
        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        
        formData.delete('policies');
        for (var i = 0; i < data.policies.length; i++)
            formData.append('policiesIDs', data.policies[i]);

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
                    <div className="divInput">
                        <div className="inputTitle">Policies</div>
                        <InputMultyplyObject
                            id="rolePolicies"
                            data={props.value.policies}
                            placeholder="Pollicies"
                            addValue={e => props.addList('policies', e)}
                            removeValue={e => props.removeList('policies', e)}
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
                <div className='pageTitle'> Edit Roles</div>
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
        addList: (name, val) => dispatch(addList(name, val)),
        removeList: (name, val) => dispatch(removeList(name, val)),
        clearState: (data) => dispatch(clearState(data)),
        loadParameters: () => dispatch(loadParameters()),
        load: (id) => dispatch(load(id)),
    }
 }

const mapStateToProps = (state) => ( console.log("mapStateToProps"),{
    
    value: state.editRole,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditRole);
