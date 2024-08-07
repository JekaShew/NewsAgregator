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

const EditWeather = (props) => {

    const city = useInput(props.value.editWeather.city.value, {isEmpty:true, minLength:2});

    const navigate = useNavigate();
    const params = useParams();
    const [state, setValue] = useState({ AddOrChange: "", Loading: true, });

    const addORchangeBtn = () => {
        let disabled = false;
        if(!city.inputValid)
            disabled = true;
        else
        disabled = false;

        if (state.AddOrChange == "Add")
            return (<button disabled={disabled} className="btnAddChange" onClick={() => addWeather()}>Add</button>);
        else if (state.AddOrChange == "Change")
            return (<button disabled={disabled} className="btnAddChange" onClick={() => changeWeather()}>Change</button>);

    }

    const goToList = () => {
        navigate("/Weathers");
    }

    const changeWeather = () => {
        let data = Object.fromEntries(Object.entries(props.value.editWeather).map(e => [e[0], e[1].value]));
        data.temperatureCommon = Number.parseInt(data.temperatureCommon);
        data.temperatureMorning = Number.parseInt(data.temperatureMorning);
        data.temperatureDay = Number.parseInt(data.temperatureDay);
        data.temperatureEvening = Number.parseInt(data.temperatureEvening);
        data.temperatureNight = Number.parseInt(data.temperatureNight);
        data.humidity = Number.parseInt(data.humidity);
        data.pressure = Number.parseInt(data.pressure);

        data.wind = Number.parseFloat(data.wind);

        data.weatherStatusCommonId = data.weatherStatusCommon.id;
        data.weatherStatusMorningId = data.weatherStatusMorning.id;
        data.weatherStatusDayId = data.weatherStatusDay.id;
        data.weatherStatusEveningId = data.weatherStatusEvening.id;
        data.weatherStatusNightId = data.weatherStatusNight.id;
        data.id = params.id;

        let formData = new FormData();

        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        props.save(formData);
    }

    const addWeather = () => {
        let data = Object.fromEntries(Object.entries(props.value.editWeather).map(e => [e[0], e[1].value]));
        data.temperatureCommon = Number.parseInt(data.temperatureCommon);
        data.temperatureMorning = Number.parseInt(data.temperatureMorning);
        data.temperatureDay = Number.parseInt(data.temperatureDay);
        data.temperatureEvening = Number.parseInt(data.temperatureEvening);
        data.temperatureNight = Number.parseInt(data.temperatureNight);
        data.humidity = Number.parseInt(data.humidity);
        data.pressure = Number.parseInt(data.pressure);

        data.wind = Number.parseFloat(data.wind);

        data.weatherStatusCommonId = data.weatherStatusCommon.id;
        data.weatherStatusMorningId = data.weatherStatusMorning.id;
        data.weatherStatusDayId = data.weatherStatusDay.id;
        data.weatherStatusEveningId = data.weatherStatusEvening.id;
        data.weatherStatusNightId = data.weatherStatusNight.id;
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

        if (state.Loading == false) {
            console.log(state.Loading);
            console.log(props.value.editWeather);
            return (
                <div className="editPageInputs">
                    <div className="divInput">
                        <div className="inputTitle">City</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="City"
                            value={city.value}
                            onChange={(e) => city.onChange(e,props.select,"city")}
                            onBlur={(e) => city.onBlur(e)}
                        />
                    </div>
                    {
                        renderValidationMessages(city)
                    }
                    <div className="divInput">
                        <div className="inputTitle">Temperature Morning</div>
                        <input
                            className="input"
                            type="number"
                            placeholder="Temperature Morning"
                            value={props.value.editWeather.temperatureMorning.value}
                            onChange={(e) => props.select("temperatureMorning", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Temperature Day</div>
                        <input
                            className="input"
                            type="number"
                            placeholder="Temperature Day"
                            value={props.value.editWeather.temperatureDay.value}
                            onChange={(e) => props.select("temperatureDay", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Temperature Evening</div>
                        <input
                            className="input"
                            type="number"
                            placeholder="Temperature Evening"
                            value={props.value.editWeather.temperatureEvening.value}
                            onChange={(e) => props.select("temperatureEvening", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Temperature Night</div>
                        <input
                            className="input"
                            type="number"
                            placeholder="Temperature Night"
                            value={props.value.editWeather.temperatureNight.value}
                            onChange={(e) => props.select("temperatureNight", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Temperature Common</div>
                        <input
                            className="input"
                            type="number"
                            placeholder="Temperature Common"
                            value={props.value.editWeather.temperatureCommon.value}
                            onChange={(e) => props.select("temperatureCommon", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Date</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Date"
                            value={props.value.editWeather.date.value}
                            onChange={(e) => props.select("date", e.target.value)}
                        />
                    </div>

                    <div className="divInput">
                        <div className="inputTitle">Percipitaion</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Percipitaion"
                            value={props.value.editWeather.percipitaion.value}
                            onChange={(e) => props.select("percipitaion", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Wind</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Wind"
                            value={props.value.editWeather.wind.value}
                            onChange={(e) => props.select("wind", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Wind Direction</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Wind Direction"
                            value={props.value.editWeather.windDirection.value}
                            onChange={(e) => props.select("windDirection", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Pressure</div>
                        <input
                            className="input"
                            type="number"
                            placeholder="Pressure"
                            value={props.value.editWeather.pressure.value}
                            onChange={(e) => props.select("pressure", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Humidity</div>
                        <input
                            className="input"
                            type="number"
                            placeholder="Humidity"
                            value={props.value.editWeather.humidity.value}
                            onChange={(e) => props.select("humidity", e.target.value)}
                        />
                    </div>

                    <div className="divInput">
                        <div className="inputTitle">Weather Status Morning</div>
                        <InputObject
                            id="weatherStatusMorning"
                            value={props.value.editWeather.weatherStatusMorning.value.id}
                            options={props.value.editWeather.weatherStatuses.value}
                            placeholder="Weather Status Morning"
                            onClick={(val, text) => props.selectParameter('weatherStatusMorning', val, text)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Weather Status Day</div>
                        <InputObject
                            id="weatherStatusDay"
                            value={props.value.editWeather.weatherStatusDay.value.id}
                            options={props.value.editWeather.weatherStatuses.value}
                            placeholder="Weather Status Day"
                            onClick={(val, text) => props.selectParameter('weatherStatusDay', val, text)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Weather Status Evening</div>
                        <InputObject
                            id="weatherStatusEvening"
                            value={props.value.editWeather.weatherStatusEvening.value.id}
                            options={props.value.editWeather.weatherStatuses.value}
                            placeholder="Weather Status Evening"
                            onClick={(val, text) => props.selectParameter('weatherStatusEvening', val, text)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Weather Status Night</div>
                        <InputObject
                            id="weatherStatusNight"
                            value={props.value.editWeather.weatherStatusNight.value.id}
                            options={props.value.editWeather.weatherStatuses.value}
                            placeholder="Weather Status Night"
                            onClick={(val, text) => props.selectParameter('weatherStatusNight', val, text)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Weather Status Common</div>
                        <InputObject
                            id="weatherStatusCommon"
                            value={props.value.editWeather.weatherStatusCommon.value.id}
                            options={props.value.editWeather.weatherStatuses.value}
                            placeholder="Weather Status Common"
                            onClick={(val, text) => props.selectParameter('weatherStatusCommon', val, text)}
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
            setValue({ AddOrChange: "Change", Loading: props.value.editWeather.loading });
        }
        else if (params.id == null) {
            setValue({ AddOrChange: "Add", Loading: props.value.editWeather.loading });
        }
        console.log(state.AddOrChange);
    }, [props.value.editWeather.loading]);

    useLayoutEffect(() => {
        beforeRender();
    }, []);


    console.log(props);
    return (

        <Wrapper>
            <div className="editPage">
                <div className="pageTitle">Edit Weather</div>
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
        add: (newWeather) => dispatch(add(newWeather)),
        save: (updatedWeather) => dispatch(save(updatedWeather)),
        loadParameters: () => dispatch(loadParameters()),
        load: (id) => dispatch(load(id)),
        clearState: (data) => dispatch(clearState(data)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.editWeather,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditWeather);