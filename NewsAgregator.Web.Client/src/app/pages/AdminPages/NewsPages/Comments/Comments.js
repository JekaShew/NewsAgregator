import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { loadData } from './actions';
import '../../../AdminPages/EditPage.css';

const Comments = (props) => {

    const navigate = useNavigate();

    const btnAddClick = () => {
        navigate('/EditComment/');
    }

    const btnEditSelectedClick = (id) => {
        navigate('/EditComment/id:' + id);
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
                <div className="pageTitle"> Comments </div>
                <button className="btnAddChange" onClick={() => btnAddClick}>Add</button>
                <table>
                    <thead>
                        <tr>
                            <th>Action</th>
                            <th>Text</th>
                            <th>Date</th>
                            <th>Account</th>
                            <th>News</th>

                        </tr>
                    </thead>
                    <tbody>
                        {props.value.comments.value.map(x => (
                            <tr key={x.id}>
                                <td>
                                    <button className="btnAddChange" onClick={() => btnEditSelectedClick(x.id.value)}>Edit</button>
                                </td>
                                <td>{x.text.value}</td>
                                <td>{x.date.value}</td>
                                <td>{x.account.value}</td>
                                <td>{x.news.value}</td>   
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

    value: state.comments,
});

export default connect(mapStateToProps, mapDispatchToProps)(Comments);