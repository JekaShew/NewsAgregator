import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { loadData } from './actions';
import '../../../AdminPages/EditPage.css';

const Weathers = (props) => {

    const navigate = useNavigate();

    const btnAddClick = () => {
        navigate('/EditWeather/');
    }

    const btnEditSelectedClick = (id) => {
        navigate('/EditWeather/id:' + id);
    }

    const beforeRender = () => {
        props.loadData();
    }


    useEffect(() => {
        beforeRender();
    }, []);


    return (
        <Wrapper>
            <div className="editPage">
                <div className="pageTitle"> Weathers </div>
                <button className="btnAddChange" onClick={() => btnAddClick}>Add</button>
                <table>
                    <thead>
                        <tr>
                            <th>Action</th>
                            <th>City</th>
                            <th>Temperature Morning</th>
                            <th>Temperature Day</th>
                            <th>Temperature Evening</th>
                            <th>Temperature Night</th>
                            <th>Temperature Common</th>
                            <th>Date</th>
                            <th>Percipitation</th>
                            <th>Wind</th>
                            <th>Wind Direction</th>
                            <th>Pressure</th>
                            <th>Weather Status Morning</th>
                            <th>Weather Status Day</th>
                            <th>Weather Status Evening</th>
                            <th>Weather Status Night</th>
                            <th>Weather Status Common</th>
                        </tr>
                    </thead>
                    <tbody>
                        {props.value.weathers.value.map(x => (
                            <tr key={x.id}>
                                <td>
                                    <button className="btnAddChange" onClick={() => btnEditSelectedClick(x.id.value)}>Edit</button>
                                </td>
                                <td>{x.city.value}</td>
                                <td>{x.temperatureMorning.value}</td>
                                <td>{x.temperatureDay.value}</td>
                                <td>{x.temperatureEvening.value}</td>
                                <td>{x.temperatureNight.value}</td>
                                <td>{x.temperatureCommon.value}</td>
                                <td>{x.date.value}</td>
                                <td>{x.percipittaion.value}</td>
                                <td>{x.wind.value}</td>
                                <td>{x.windDirection.value}</td>
                                <td>{x.pressure.value}</td>
                                <td>{x.humidity.value}</td>
                                <td>{x.weatherStatusMorning.value}</td>
                                <td>{x.weatherStatusDay.value}</td>
                                <td>{x.weatherStatusEvening.value}</td>
                                <td>{x.weatherStatusNight.value}</td>
                                <td>{x.weatherStatusCommon.value}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </Wrapper>
    );
};

const mapDispatchToProps = dispatch => {
    return {

        loadData: () => dispatch(loadData()),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.weathers,
});

export default connect(mapStateToProps, mapDispatchToProps)(Weathers);