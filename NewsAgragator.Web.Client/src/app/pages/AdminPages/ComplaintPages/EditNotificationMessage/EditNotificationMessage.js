import React, { useEffect, useLayoutEffect, useState } from 'react';
import { connect } from 'react-redux';
import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { add, save, select, selectParameter, clearState, load, loadParameters } from './actions';
import InputObject from '../../../../customComponents/InputObject/InputObject';
import '../../../AdminPages/EditPage.css';


const EditNotificationMessage = (props) => {

    const navigate = useNavigate();
    const params = useParams();
    const [state, setValue] = useState({ AddOrChange: "", Loading: true, });

    const addORchangeBtn = () => {
        if (state.AddOrChange == "Add")
            return (<button className="btnAddChange" onClick={() => addNotificationMessage()}>Add</button>);
        else if (state.AddOrChange == "Change")
            return (<button className="btnAddChange" onClick={() => changeNotificationMessage()}>Change</button>);

    }

    const goToList = () => {
        navigate("/NotificationMessages");
    }

    const changeNotificationMessage = () => {
        let data = Object.fromEntries(Object.entries(props.value.editNotificationMessage).map(e => [e[0], e[1].value]));
        data.userId = data.user.id;
        data.administratorId = data.administrator.id;
        data.id = params.id;

        let formData = new FormData();

        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }
        //formData.append('id', params.id);
        //formData.append('userId', props.value.editNotificationMessage.user.value.id);
        //formData.append('administratorId', props.value.editNotificationMessage.administrator.value.id);

        props.save(formData);
    }

    const addNotificationMessage = () => {
        let data = Object.fromEntries(Object.entries(props.value.editNotificationMessage).map(e => [e[0], e[1].value]));
        data.userId = data.user.id;
        data.administratorId = data.administrator.id;
        data.id = null;


        let formData = new FormData();

        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }
        //formData.append('userId', props.value.editNotificationMessage.user.value.id);
        //formData.append('administratorId', props.value.editNotificationMessage.administrator.value.id);
        //formData.append('id', null);

        props.add(formData);
    };

    const renderInputs = () => {
        console.log("renderInputs");
        console.log(props.value.editNotificationMessage);
        if (state.Loading == false) {

            return (
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
                            value={props.value.editNotificationMessage.user.value.id}
                            options={props.value.editNotificationMessage.accounts.value}
                            placeholder="User"
                            onClick={(val, text) => props.selectParameter('user', val, text)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Administrator</div>
                        <InputObject
                            id="roles"
                            value={props.value.editNotificationMessage.administrator.value.id}
                            options={props.value.editNotificationMessage.accounts.value}
                            placeholder="Administrator"
                            onClick={(val, text) => props.selectParameter('administrator', val, text)}
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
            setValue({ AddOrChange: "Change", Loading: props.value.editNotificationMessage.loading });
        }
        else if (params.id == null) {
            setValue({ AddOrChange: "Add", Loading: props.value.editNotificationMessage.loading });
        }
        console.log(state.AddOrChange);
    }, [props.value.editNotificationMessage.loading]);

    useLayoutEffect(() => {
        beforeRender();
    }, []);


    console.log(props);
    return (

        <Wrapper>
            <div className="editPage">
                <div className="pageTitle">Edit Notification Message </div>
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
        add: (newNotificationMessage) => dispatch(add(newNotificationMessage)),
        save: (updatedNotificationMessage) => dispatch(save(updatedNotificationMessage)),
        loadParameters: () => dispatch(loadParameters()),
        load: (id) => dispatch(load(id)),
        clearState: (data) => dispatch(clearState(data)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.editNotificationMessage,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditNotificationMessage);