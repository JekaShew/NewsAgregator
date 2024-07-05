import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { loadData } from './actions';
import '../../../AdminPages/EditPage.css';

const AccountStatuses = (props) => {

    const navigate = useNavigate();

    const btnAddClick = () => {
        navigate('/EditAccountStatus/');
    }

    const btnEditSelectedClick = (id) => {
        navigate('/EditAccountStatus/' + id);
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
                <div className="pageTitle"> Account Statuses </div>
                <button className="btnAddChange" onClick={() => btnAddClick()}>Add</button>
                <table className="table table-hover">
                    <thead>
                        <tr>
                            <th scope="col">Action</th>
                            <th scope="col">Title</th>
                            <th scope="col">Description</th>
                            
                        </tr>
                    </thead>
                    <tbody>
                        {props.value.accountStatuses.value.map(x => (
                            <tr key={x.id}>
                                <td scope="row">
                                    <button className="btnAddChange" onClick={() => btnEditSelectedClick(x.id.value)}>Edit</button>
                                </td>
                                <td>{x.title.value}</td>
                                <td>{x.description.value}</td>
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

    value: state.accountStatuses,
});

export default connect(mapStateToProps, mapDispatchToProps)(AccountStatuses);