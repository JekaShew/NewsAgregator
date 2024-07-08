import React, { useEffect, useLayoutEffect, useState } from 'react';
import { connect } from 'react-redux';
import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { add, save, select, selectParameter, clearState, load, loadParameters } from './actions';
import InputObject from '../../../../customComponents/InputObject/InputObject';
import '../../../AdminPages/EditPage.css';


const EditAccount = (props) => {

    const navigate = useNavigate();
    const params = useParams();
    const [state, setValue] = useState({ AddOrChange: "", Loading: true, });

    const addORchangeBtn = () => {
        
        console.log(state.AddOrChange);
        if (state.AddOrChange == "Add")
            return (<button className="btnAddChange" onClick={() => addAccount()}>Add</button>);
        else if (state.AddOrChange == "Change")
            return (<button className="btnAddChange" onClick={() => changeAccount()}>Change</button>);

    }

    const goToList = () => {
        navigate("/Accounts");
    }

    const changeAccount = () => {
        let data = Object.fromEntries(Object.entries(props.value.editAccount).map(e => [e[0], e[1].value]));
        data.desiredNewsRating = Number.parseInt(data.desiredNewsRating);
        data.accountStatusId = data.accountStatus.id;
        data.roleId = data.role.id;
        data.id = params.id;

        console.log(data)
        console.log(data.accountStatus.id);
        console.log(props.value.editAccount.accountStatus.value.id);
        let formData = new FormData();

        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }
        //formData.append('id', params.id);
        //formData.append('accountStatusId', data.accountStatus.id);
        //formData.append('roleId', props.value.editAccount.role.id);

        props.save(formData);
    }

    const addAccount = () => {
        let data = Object.fromEntries(Object.entries(props.value.editAccount).map(e => [e[0], e[1].value]));
        data.desiredNewsRating = Number.parseInt(data.desiredNewsRating);
        data.accountStatusId = data.accountStatus.id;
        data.roleId = data.role.id;
        data.id = null;
        console.log("AddAccount");
        console.log(props.value.editAccount);
        console.log(data);
        let formData = new FormData();

        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }
        //formData.append('accountStatusId', props.value.editAccount.accountStatus.value.id);
        //formData.append('roleId', props.value.editAccount.role.value.id);
        //formData.append('id', null);

        props.add(formData);
    };

    const renderInputs = () => {
        console.log("renderInputs");
        
        if (state.Loading == false) {
            console.log(state.Loading);
            console.log(props.value.editAccount);
            return (
                <div className="editPageInputs">
                    <div className="divInput">
                        <div className="inputTitle">UserName</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="UserName"
                            value={props.value.editAccount.userName.value}
                            onChange={(e) => props.select("userName", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">FIO</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="FIO"
                            value={props.value.editAccount.fio.value}
                            onChange={(e) => props.select("fio", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Email</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Email"
                            value={props.value.editAccount.email.value}
                            onChange={(e) => props.select("email", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Phone</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Phone"
                            value={props.value.editAccount.phone.value}
                            onChange={(e) => props.select("phone", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">DesiredNewsRating</div>
                        <input
                            className="input"
                            type="number"
                            placeholder="Desired News Rating"
                            value={props.value.editAccount.desiredNewsRating.value}
                            onChange={(e) => props.select("desiredNewsRating", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Login</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Login"
                            value={props.value.editAccount.login.value}
                            onChange={(e) => props.select("login", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Password</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Password"
                            value={props.value.editAccount.password.value}
                            onChange={(e) => props.select("password", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Account Status</div>
                        <InputObject
                            id="accountStatuses"
                            value={props.value.editAccount.accountStatus.value.id}
                            options={props.value.editAccount.accountStatuses.value}
                            placeholder="Account Status"
                            onClick={(val, text) => props.selectParameter('accountStatus', val,text)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Role</div>
                        <InputObject
                            id="roles"
                            value={props.value.editAccount.role.value.id}
                            options={props.value.editAccount.roles.value}
                            placeholder="Role"
                            onClick={(val,text) => props.selectParameter('role', val, text)}
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

    //useEffect(() => {
    //    console.log("NewUseEffect");
        
    //    if (params.id != null) {
    //        setValue({ AddOrChange: "Change", Loading: state.Loading });
    //    }
    //    else if (params.id == null) {
    //        setValue({ AddOrChange: "Add", Loading: state.Loading });
    //    }
    //    console.log(state.AddOrChange);
    //});


    useEffect(() => {
        console.log("propsLoading changed");
            if (params.id != null) {
                setValue({ AddOrChange: "Change", Loading: props.value.editAccount.loading });
            }
            else if (params.id == null) {
                setValue({ AddOrChange: "Add", Loading: props.value.editAccount.loading });
            }
        /*setValue({ AddOrChange: state.AddOrChange, Loading: props.value.editAccount.loading });*/
        console.log(state.AddOrChange);
    }, [props.value.editAccount.loading]);

    useLayoutEffect(() => {
        beforeRender();
    }, []);

    return (

        <Wrapper>
            <div className="editPage">
                <div className="pageTitle">Edit Account </div>
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
        add: (newAccount) => dispatch(add(newAccount)),
        save: (updatedAccount) => dispatch(save(updatedAccount)),
        loadParameters: () => dispatch(loadParameters()),
        load: (id) => dispatch(load(id)),
        clearState: (data) => dispatch(clearState(data)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.editAccount,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditAccount);