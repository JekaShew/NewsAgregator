import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import ModalConfirmation from '../../../../customComponents/ModalConfirmation/ModalConfirmation';
import { loadData, remove } from './actions';
import '../../../AdminPages/EditPage.css';

const WeatherStatuses = (props) => {

    const [confirmationDeleting, setConfirmationDeleting] = useState({ Id: '', Title: '', ConfirmationModalShow: false });
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    const btnAddClick = () => {
        navigate('/EditWeatherStatus/');
    }

    const btnGoToReferenceBooks = () => {
        navigate('/ReferenceBooks');
    }

    const btnEditSelectedClick = (id) => {
        navigate('/EditWeatherStatus/' + id);
    }
    const confirmationDeletingModalShow = (id, title) => {
        setConfirmationDeleting({ Id: id, Title: title, ConfirmationModalShow: true });
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

        console.log(props.value);

        if (loading == false) {
            return (
                <table className="table table-bordered table-hover">
                    <thead>
                        <tr>
                            <th className="tableRow" scope="col">Action</th>
                            <th scope="col">Title</th>
                            <th scope="col">Description</th>

                        </tr>
                    </thead>
                    <tbody>
                        {props.value.weatherStatuses.value.map(x => (
                            <tr className="tableRow" key={x.id}>

                                <th scope="row" className=" tableRowActions ">
                                    <button className="btnAddChange" onClick={() => btnEditSelectedClick(x.id)}>Edit</button> <div style={{ fontSize: '2rem', alignContent: 'center' }}> | </div>
                                    <button onClick={() => confirmationDeletingModalShow(x.id, x.title)} type="button" className="btnDelete" data-toggle="modal" data-target="#deleteModal">
                                        Delete
                                    </button>
                                </th>
                                <td>{x.title != null ? x.title : "No data"}</td>
                                <td>{x.description != null ? x.description : "No data"}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>)
        }
        else
            return
        (<div className="items loading">
            <FontAwesomeIcon icon={faSpinner} />
        </div>)
    }



    const beforeRender = () => {
        setLoading(true);
        props.loadData();
    }

    useEffect(() => {
        setLoading(props.value.weatherStatuses.loading);
    }, [props.value.weatherStatuses.loading]);

    useEffect(() => {
        beforeRender();
    }, []);



    return (
        <Wrapper>
            <div className="editPage">
                <div className="pageTitle"> Weather Statuses </div>
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

        loadData: () => dispatch(loadData()),
        remove: (id) => dispatch(remove(id)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.weatherStatuses,
});

export default connect(mapStateToProps, mapDispatchToProps)(WeatherStatuses);