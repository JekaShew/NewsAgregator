import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { loadData } from './actions';
import '../../../AdminPages/EditPage.css';

const ComplaintStatuses = (props) => {

    const navigate = useNavigate();

    const btnAddClick = () => {
        navigate('/EditCompalintStatus/');
    }

    const btnEditSelectedClick = (id) => {
        navigate('/EditCompalintStatus/id:' + id);
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
                <div className="pageTitle"> Complaint Statuses </div>
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
                        {props.value.complaintStatuses.value.map(x => (
                            <tr key={x.id}>
                                <td>
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

    value: state.complaintStatuses,
});

export default connect(mapStateToProps, mapDispatchToProps)(ComplaintStatuses);