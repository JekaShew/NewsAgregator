import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import ModalConfirmation from '../../../../customComponents/ModalConfirmation/ModalConfirmation';
import { loadData, remove } from './actions';
import '../../../AdminPages/EditPage.css';

const Weathers = (props) => {

    const [confirmationDeleting, setConfirmationDeleting] = useState({ Id: '', Title: '', ConfirmationModalShow: false });
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    const btnAddClick = () => {
        navigate('/EditWeather/');
    }

    const btnGoToReferenceBooks = () => {
        navigate('/ReferenceBooks');
    }

    const btnEditSelectedClick = (id) => {
        navigate('/EditWeather/' + id);
    }

    const confirmationDeletingModalShow = (id, city) => {
        setConfirmationDeleting({ Id: id, Title: city, ConfirmationModalShow: true });
        console.log("btnDelete");
        console.log(confirmationDeleting);
        console.log(props);

    };

    const btnDeleteClick = (id) => {
        props.remove(id);
        setConfirmationDeleting({ Id: '', Title: '', ConfirmationModalShow: false });
    }

    const btnCloseClick = () => {
        setConfirmationDeleting({ Id: '', Title: '', ConfirmationModalShow: false });
    }

    const renderTable = () => {
        console.log("RenderTable");
        console.log(props);
        console.log(props.value.editWeather);

        if (loading == false) {
            return (
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
                            <tr className="tableRow" key={x.id.value}>

                                <th scope="row" className=" tableRowActions ">
                                    <button className="btnAddChange" onClick={() => btnEditSelectedClick(x.id.value)}>Edit</button> <div style={{ fontSize: '2rem', alignContent: 'center' }}> | </div>
                                    <button onClick={() => confirmationDeletingModalShow(x.id.value, x.city.value)} type="button" className="btnDelete" data-toggle="modal" data-target="#deleteModal">
                                        Delete
                                    </button>
                                </th>
                                <td>{x.city.value != null ? x.city.value : "No data"}</td>
                                <td>{x.temperatureMorning.value != null ? x.temperatureMorning.value : "No data"}</td>
                                <td>{x.temperatureDay.value != null ? x.temperatureDay.value : "No data"}</td>
                                <td>{x.temperatureEvening.value != null ? x.temperatureEvening.value : "No data"}</td>
                                <td>{x.temperatureNight.value != null ? x.temperatureNight.value : "No data"}</td>
                                <td>{x.temperatureCommon.value != null ? x.temperatureCommon.value : "No data"}</td>
                                <td>{x.date.value != null ? x.date.value : "No data"}</td>
                                <td>{x.percipitaion.value != null ? x.percipitaion.value : "No data"}</td>
                                <td>{x.wind.value != null ? x.wind.value : "No data"}</td>
                                <td>{x.windDirection.value != null ? x.windDirection.value : "No data"}</td>
                                <td>{x.pressure.value != null ? x.pressure.value : "No data"}</td>
                                <td>{x.humidity.value != null ? x.humidity.value : "No data"}</td>
                                <td>{x.weatherStatusMorning.id.value != null ? x.weatherStatusMorning.text.value : "No data"}</td>
                                <td>{x.weatherStatusDay.id.value != null ? x.weatherStatusDay.text.value : "No data"}</td>
                                <td>{x.weatherStatusEvening.id.value != null ? x.weatherStatusEvening.text.value : "No data"}</td>
                                <td>{x.weatherStatusNight.id.value != null ? x.weatherStatusNight.text.value : "No data"}</td>
                                <td>{x.weatherStatusCommon.id.value != null ? x.weatherStatusCommon.text.value : "No data"}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>
                 )
        }
        else
            return
        (<div className="items loading">
            <FontAwesomeIcon icon={faSpinner} />
        </div>)
    }

    const beforeRender = () => {
        console.log("BeforeRender");
        setLoading(true);
        props.loadData();
    }

    useEffect(() => {
        setLoading(props.value.weathers.loading);
    }, [props.value.weathers.loading]);

    useEffect(() => {
        beforeRender();
    }, []);



    return (
        <Wrapper>
            <div className="editPage">
                <div className="pageTitle"> Weathers </div>
                <div className="btnsAboveTable">
                    <button className="btnAddChange" style={{ width: '12rem' }} onClick={() => btnGoToReferenceBooks()}>Reference Books</button>
                    <button className="btnAddChange" onClick={() => btnAddClick()}>Add</button>
                </div>
                {renderTable()}

            </div>
            <div style={{ display: confirmationDeleting.ConfirmationModalShow ? 'block' : 'none' }}>
                <ModalConfirmation

                    id={confirmationDeleting.Id}
                    title={confirmationDeleting.Title}
                    onDelete={btnDeleteClick}
                    onClose={btnCloseClick}
                />
            </div>
        </Wrapper>
    );
};

const mapDispatchToProps = dispatch => {
    return {

        remove: (id) => dispatch(remove(id)),
        loadData: () => dispatch(loadData()),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.weathers,
});

export default connect(mapStateToProps, mapDispatchToProps)(Weathers);