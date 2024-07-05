import React from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { select, add, save, loadParameters, loadData, clearState } from './actions';
import InputObject from '../../../../customComponents/InputObject/InputObject';
import '../../../AdminPages/EditPage.css';


const EditAccount = (props) => {

    const navigate = useNavigate();

    const addORchangeBtn = (addORchange) => {
        if (addORchange == "Add")
            return (<button className="btnAddChange" onClick={() => addAccount}>Add</button>);
        else if (addORchange == "Change")
            return (<button className="btnAddChange" onClick={() => changeAccount}>Change</button>);

    }

    const changeAccount = () => {
        const data = Object.fromEntries(Object.entries(props.value.editAccount).map(([key, value]) => [key, value.value]));

        const formData = new FormData();

        for (const key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        props.change(formData);
        navigate("/Accounts");
    }

    const addAccount = () => {
        const data = Object.fromEntries(Object.entries(props.value.editAccount).map(([key, value]) => [key, value.value]));

        const formData = new FormData();

        for (const key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        formData.delete('id');
        formData.append('id', null);

        props.add(formData);
        navigate("/Accounts");
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
                <div className="pageTitle">Edit Account </div>
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
                            value={props.value.editAccount.fIO.value}
                            onChange={(e) => props.select("fIO", e.target.value)}
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
                            type="text"
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
                        value={props.value.editAccount.accountStatus.value}
                        options={props.value.editAccount.accountStatus.options}
                        placeholder="Account Status"
                        onClick={(val) => this.props.select('accountStatus', val)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Role</div>
                    <InputObject
                        id="roles"
                        value={props.value.editAccount.accountRole.value}
                        options={props.value.editAccount.accountRole.options}
                        placeholder="Role"
                        onClick={(val) => this.props.select('accountRole', val)}
                        />
                    </div>
                </div>
                <button className="btnAddChange" onClick={() => addAccount}>Add</button>
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
        add: (newAccount) => dispatch(add(newAccount)),
        save: (updatedAccount) => dispatch(save(updatedAccount)),
        loadParameters: () => dispatch(loadParameters()),
        loadData: (id) => dispatch(loadData(id)),
        clearState: (data) => dispatch(clearState(data)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.editAccount,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditAccount);