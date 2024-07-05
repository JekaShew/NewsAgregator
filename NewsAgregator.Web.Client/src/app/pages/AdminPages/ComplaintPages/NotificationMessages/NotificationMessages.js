import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { loadData } from './actions';
import '../../../AdminPages/EditPage.css';

const NotificationMessages = (props) => {

    const navigate = useNavigate();

    const btnAddClick = () => {
        navigate('/EditNotificationMessage/');
    }

    const btnEditSelectedClick = (id) => {
        navigate('/EditNotificationMessage/id:' + id);
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
                <div className="pageTitle"> EditNotification Messages </div>
                <button className="btnAddChange" onClick={() => btnAddClick}>Add</button>
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
                            <tr key={x.id}>
                                <td>
                                    <button className="btnAddChange" onClick={() => btnEditSelectedClick(x.id.value)}>Edit</button>
                                </td>
                                <td>{x.title.value}</td>
                                <td>{x.text.value}</td>
                                <td>{x.user.value}</td>
                                <td>{x.administrator.value}</td>
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

    value: state.notificationMessages,
});

export default connect(mapStateToProps, mapDispatchToProps)(NotificationMessages);