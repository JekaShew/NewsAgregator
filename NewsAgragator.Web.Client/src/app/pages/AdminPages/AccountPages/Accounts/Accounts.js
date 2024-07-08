import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import ModalConfirmation from '../../../../customComponents/ModalConfirmation/ModalConfirmation';
import { loadData, remove } from './actions';
import '../../../AdminPages/EditPage.css';

const Accounts = (props) => {

    const [confirmationDeleting, setConfirmationDeleting] = useState({ Id: '', Title: '', ConfirmationModalShow: false });
    const [loading,setLoading] = useState(true);
    const navigate = useNavigate();

    const btnAddClick = () => {
        navigate('/EditAccount/');
    }
    const btnGoToReferenceBooks = () => {
        navigate('/ReferenceBooks');
    }


    const btnEditSelectedClick = (id) => {
        navigate('/EditAccount/' + id);
    }

    const confirmationDeletingModalShow = (id, userName) => {
        setConfirmationDeleting({ Id: id, Title: userName, ConfirmationModalShow: true });
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
        console.log(props.value);

        if (loading == false) {
            return (
                <table className="table table-bordered table-hover">
                    <thead>
                        <tr className="tableRow">
                            <th>Action</th>
                            <th>UserName</th>
                            <th>FIO</th>
                            <th>Email</th>
                            <th>Phone</th>
                            <th>Desired Rating</th>
                            <th>Login</th>
                            <th>Account Status</th>
                            <th>Account Role</th>
                        </tr>
                    </thead>
                    <tbody>
                        {props.value.accounts.value.map(x => (
                            <tr className="tableRow" key={x.id.value}>

                                <th scope="row" className=" tableRowActions ">
                                    <button className="btnAddChange" onClick={() => btnEditSelectedClick(x.id.value)}>Edit</button> <div style={{ fontSize: '2rem', alignContent: 'center' }}> | </div>
                                    <button onClick={() => confirmationDeletingModalShow(x.id.value, x.userName.value)} type="button" className="btnDelete" data-toggle="modal" data-target="#deleteModal">
                                        Delete
                                    </button>
                                </th>

                                <td>{x.userName.value != null ? x.userName.value : "No data"}</td>
                                <td>{x.fio.value != null ? x.fio.value : "No data"}</td>
                                <td>{x.email.value != null ? x.email.value : "No data"}</td>
                                <td>{x.phone.value != null ? x.phone.value : "No data"}</td>
                                <td>{x.desiredNewsRating.value != null ? x.desiredNewsRating.value : "No data"}</td>
                                <td>{x.login.value != null ? x.login.value : "No data"}</td>
                                <td>{x.accountStatus.id.value != null ? x.accountStatus.text.value : "No data"}</td>
                                <td>{x.role.id.value != null ? x.role.text.value : "No data"}</td>
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
        setLoading(props.value.accounts.loading);
    }, [props.value.accounts.loading]);

    useEffect(() => {
        beforeRender();
    }, []);


    return (
        <Wrapper>
            <div className="editPage">
                <div className="pageTitle"> Accounts </div>
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

    value: state.accounts,
});

export default connect(mapStateToProps, mapDispatchToProps)(Accounts);