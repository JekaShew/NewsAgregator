import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { loadData } from './actions';
import '../../../AdminPages/EditPage.css';

const Complaints = (props) => {

    const navigate = useNavigate();

    const btnAddClick = () => {
        navigate('/EditComplaint/');
    }

    const btnEditSelectedClick = (id) => {
        navigate('/EditComplaint/id:' + id);
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
                <div className="pageTitle"> Complaints </div>
                <button className="btnAddChange" onClick={() => btnAddClick}>Add</button>
                <table>
                    <thead>
                        <tr>
                            <th>Action</th>
                            <th>Title</th>
                            <th>Text</th>
                            <th>Comment</th>
                            <th>News</th>
                            <th>Complaint Status</th>
                            <th>Complaint Type</th>
                            <th>User</th>
                            <th>Administrator</th>

                        </tr>
                    </thead>
                    <tbody>
                        {props.value.complaints.value.map(x => (
                            <tr key={x.id}>
                                <td>
                                    <button className="btnAddChange" onClick={() => btnEditSelectedClick(x.id.value)}>Edit</button>
                                </td>
                                <td>{x.title.value}</td>
                                <td>{x.text.value}</td>
                                <td>{x.comment.value}</td>
                                <td>{x.news.value}</td>
                                <td>{x.complaintStatus.value}</td>
                                <td>{x.complaintType.value}</td>
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

    value: state.complaints,
});

export default connect(mapStateToProps, mapDispatchToProps)(Complaints);