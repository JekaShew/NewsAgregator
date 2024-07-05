import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { loadData } from './actions';
import '../../../AdminPages/EditPage.css';

const Accounts = (props) => {

    const navigate = useNavigate();

    const btnAddClick = () => {
        navigate('/EditAccount/');
    }

    const btnEditSelectedClick = (id) => {
        navigate('/EditAccount/id:' + id);
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
                <div className="pageTitle"> Accounts </div>
                <button className="btnAddChange" onClick={() => btnAddClick}>Add</button>
                <table>
                    <thead>
                        <tr>
                            <th>Action</th>
                            <th>Title</th>
                            <th>Description</th>

                        </tr>
                    </thead>
                    <tbody>
                        {props.value.accounts.value.map(x => (
                            <tr key={x.id}>
                                <td>
                                    <button className="btnAddChange" onClick={() => btnEditSelectedClick(x.id.value)}>Edit</button>
                                </td>
                                <td>{x.title.value}</td>
                                <td>{x.userName.value}</td>
                                <td>{x.fIO.value}</td>
                                <td>{x.email.value}</td>
                                <td>{x.phone.value}</td>
                                <td>{x.desiredRating.value}</td>
                                <td>{x.login.value}</td>
                                <td>{x.accountStatus.value}</td>
                                <td>{x.accountRole.value}</td>
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

    value: state.accounts,
});

export default connect(mapStateToProps, mapDispatchToProps)(Accounts);