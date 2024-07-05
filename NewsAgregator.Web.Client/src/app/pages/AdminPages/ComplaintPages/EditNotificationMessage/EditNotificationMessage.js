import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { select, add, save, loadParameters, loadData, clearState } from './actions';
import InputObject from '../../../../customComponents/InputObject/InputObject';
import '../../../AdminPages/EditPage.css';


const EditNotificationMessage = (props) => {

    const navigate = useNavigate();

    const addORchangeBtn = (addORchange) => {
        if (addORchange == "Add")
            return (<button className="btnAddChange" onClick={() => addNotificationMessage}>Add</button>);
        else if (addORchange == "Change")
            return (<button className="btnAddChange" onClick={() => changeNotificationMessage}>Change</button>);

    }

    const changeNotificationMessage = () => {
        const data = Object.fromEntries(Object.entries(props.value.editNotificationMessage).map(([key, value]) => [key, value.value]));

        const formData = new FormData();

        for (const key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        props.change(formData);
        navigate("/NotificationMessages");
    }

    const addNotificationMessage = () => {
        const data = Object.fromEntries(Object.entries(props.value.editNotificationMessage).map(([key, value]) => [key, value.value]));

        const formData = new FormData();

        for (const key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        formData.delete('id');
        formData.append('id', null);

        props.add(formData);
        navigate("/NotificationMessages");
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
                <div className="pageTitle">Edit Notification Message </div>
                <div className="editPageInputs">
                    <div className="divInput">
                        <div className="inputTitle">Title</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Title"
                            value={props.value.editNotificationMessage.title.value}
                            onChange={(e) => props.select("title", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Text</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Text"
                            value={props.value.editNotificationMessage.text.value}
                            onChange={(e) => props.select("text", e.target.value)}
                        />
                    </div>
                    
                    <div className="divInput">
                        <div className="inputTitle">User </div>
                        <InputObject
                            id="accountStatuses"
                            value={props.value.editNotificationMessage.user.value}
                            options={props.value.editNotificationMessage.user.options}
                            placeholder="User"
                            onClick={(val) => this.props.select('user', val)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Administrator</div>
                        <InputObject
                            id="roles"
                            value={props.value.editNotificationMessage.administrator.value}
                            options={props.value.editNotificationMessage.administrator.options}
                            placeholder="Administrator"
                            onClick={(val) => this.props.select('administrator', val)}
                        />
                    </div>
                </div>
                <button className="btnAddChange" onClick={() => addNotificationMessage}>Add</button>
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
        add: (newNotificationMessage) => dispatch(add(newNotificationMessage)),
        save: (updatedNotificationMessage) => dispatch(save(updatedNotificationMessage)),
        loadParameters: () => dispatch(loadParameters()),
        loadData: (id) => dispatch(loadData(id)),
        clearState: (data) => dispatch(clearState(data)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.editNotificationMessage,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditNotificationMessage);