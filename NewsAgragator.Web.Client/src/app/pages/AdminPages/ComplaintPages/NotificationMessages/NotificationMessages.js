import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import ModalConfirmation from '../../../../customComponents/ModalConfirmation/ModalConfirmation';
import { loadData, remove } from './actions';
import '../../../AdminPages/EditPage.css';

const NotificationMessages = (props) => {

    const [confirmationDeleting, setConfirmationDeleting] = useState({ Id: '', Title: '', ConfirmationModalShow: false });
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    const btnAddClick = () => {
        navigate('/EditNotificationMessage/');
    }

    const btnGoToReferenceBooks = () => {
        navigate('/ReferenceBooks');
    }

    const btnEditSelectedClick = (id) => {
        navigate('/EditNotificationMessage/' + id);
    }

    const confirmationDeletingModalShow = (id, title) => {
        setConfirmationDeleting({ Id: id, Title: title, ConfirmationModalShow: true });

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
        console.log(props.value);

        if (loading == false) {
            return (
                <table>
                    <thead>
                        <tr>
                            <th>Action</th>
                            <th>Title</th>
                            <th>Text</th>
                            <th>User</th>
                            <th>Administrator</th>

                        </tr>
                    </thead>
                    <tbody>
                        {props.value.notificationMessages.value.map(x => (
                            <tr className="tableRow" key={x.id.value}>

                                <th scope="row" className=" tableRowActions ">
                                    <button className="btnAddChange" onClick={() => btnEditSelectedClick(x.id.value)}>Edit</button> <div style={{ fontSize: '2rem', alignContent: 'center' }}> | </div>
                                    <button onClick={() => confirmationDeletingModalShow(x.id.value, x.title.value)} type="button" className="btnDelete" data-toggle="modal" data-target="#deleteModal">
                                        Delete
                                    </button>
                                </th>
                                <td>{x.title.value != null ? x.title.value : "No data"}</td>
                                <td>{x.text.value != null ? x.text.value : "No data"}</td>
                                <td>{x.user.id.value != null ? x.user.text.value : "No data"}</td>
                                <td>{x.administrator.id.value != null ? x.administrator.text.value : "No data"}</td>
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
        setLoading(props.value.notificationMessages.loading);
    }, [props.value.notificationMessages.loading]);

    useEffect(() => {
        beforeRender();
    }, []);


    return (
        <Wrapper>
            <div className="editPage">
                <div className="pageTitle"> EditNotification Messages </div>
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

    value: state.notificationMessages,
});

export default connect(mapStateToProps, mapDispatchToProps)(NotificationMessages);