import React from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { select, add, save, loadParameters, loadData, clearState } from './actions';
import InputObject from '../../../../customComponents/InputObject/InputObject';
import '../../../AdminPages/EditPage.css';


const EditWeather = (props) => {

    const navigate = useNavigate();

    const addORchangeBtn = (addORchange) => {
        if (addORchange == "Add")
            return (<button className="btnAddChange" onClick={() => addWeather}>Add</button>);
        else if (addORchange == "Change")
            return (<button className="btnAddChange" onClick={() => changeWeather}>Change</button>);

    }

    const changeWeather = () => {
        const data = Object.fromEntries(Object.entries(props.value.editWeather).map(([key, value]) => [key, value.value]));

        const formData = new FormData();

        for (const key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        props.change(formData);
        navigate("/Weathers");
    }

    const addWeather = () => {
        const data = Object.fromEntries(Object.entries(props.value.editWeather).map(([key, value]) => [key, value.value]));

        const formData = new FormData();

        for (const key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        formData.delete('id');
        formData.append('id', null);

        props.add(formData);
        navigate("/Weathers");
    };

    const beforeRender = () => {
        if (props.match.params.id) {
            props.loadData(props.match.params.id);
            addORchangeBtn("Change");
        }
        else {
            props.clearState();
            props.loadParameters();
            addORchangeBtn("Add");
        }
    }

    useEffect(() => {
        beforeRender();
    }, []);


    console.log(props);
    return (

        <Wrapper>
            <div className="editPage">
                <div className="pageTitle">Edit Weather</div>
                <div className="editPageInputs">
                    <div className="divInput">
                        <div className="inputTitle">City</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="City"
                            value={props.value.editWeather.city.value}
                            onChange={(e) => props.select("city", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Temperature Morning</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Temperature Morning"
                            value={props.value.editWeather.temperatureMorning.value}
                            onChange={(e) => props.select("temperatureMorning", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Temperature Day</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Temperature Day"
                            value={props.value.temperatureDay.email.value}
                            onChange={(e) => props.select("temperatureDay", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Temperature Evening</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Temperature Evening"
                            value={props.value.editWeather.temperatureEvening.value}
                            onChange={(e) => props.select("temperatureEvening", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Temperature Night</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Temperature Night"
                            value={props.value.editWeather.temperatureNight.value}
                            onChange={(e) => props.select("temperatureNight", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Temperature Common</div>
                        <input
                            className="input"
                            type="text"
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
                            type="text"
                            placeholder="Pressure"
                            value={props.value.editWeather.pressure.value}
                            onChange={(e) => props.select("pressure", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Humidity</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Humidity"
                            value={props.value.editWeather.humidity.value}
                            onChange={(e) => props.select("humidity", e.target.value)}
                        />
                    </div>

                    <div className="divInput">
                        <div className="inputTitle">Weather Status Morning</div>
                        <InputObject
                            id="weatherStatusMorning"
                            value={props.value.editWeather.weatherStatusMorning.value}
                            options={props.value.editWeather.weatherStatusMorning.options}
                            placeholder="Weather Status Morning"
                            onClick={(val) => this.props.select('weatherStatusMorning', val)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Weather Status Day</div>
                        <InputObject
                            id="weatherStatusDay"
                            value={props.value.editWeather.weatherStatusDay.value}
                            options={props.value.editWeather.weatherStatusDay.options}
                            placeholder="Weather Status Day"
                            onClick={(val) => this.props.select('weatherStatusDay', val)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Weather Status Evening</div>
                        <InputObject
                            id="weatherStatusEvening"
                            value={props.value.editWeather.weatherStatusEvening.value}
                            options={props.value.editWeather.weatherStatusEvening.options}
                            placeholder="Weather Status Evening"
                            onClick={(val) => this.props.select('weatherStatusEvening', val)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Weather Status Night</div>
                        <InputObject
                            id="weatherStatusNight"
                            value={props.value.editWeather.weatherStatusNight.value}
                            options={props.value.editWeather.weatherStatusNight.options}
                            placeholder="Weather Status Night"
                            onClick={(val) => this.props.select('weatherStatusNight', val)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Weather Status Common</div>
                        <InputObject
                            id="weatherStatusCommon"
                            value={props.value.editWeather.weatherStatusCommon.value}
                            options={props.value.editWeather.weatherStatusCommon.options}
                            placeholder="Weather Status Common"
                            onClick={(val) => this.props.select('weatherStatusCommon', val)}
                        />
                    </div>
                </div>
                <button className="btnAddChange" onClick={() => addWeather}>Add</button>
                {
                    addORchangeBtn(props.addORchange)
                }
            </div>
        </Wrapper>
    );
};

const mapDispatchToProps = dispatch => {
    return {
        select: (name, value) => dispatch(select(name, value)),
        add: (newWeather) => dispatch(add(newWeather)),
        save: (updatedWeather) => dispatch(save(updatedWeather)),
        loadParameters: () => dispatch(loadParameters()),
        loadData: (id) => dispatch(loadData(id)),
        clearState: (data) => dispatch(clearState(data)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.editWeather,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditWeather);