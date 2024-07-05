import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { loadData } from './actions';
import '../../../AdminPages/EditPage.css';

const News = (props) => {

    const navigate = useNavigate();

    const btnAddClick = () => {
        navigate('/EditNews/');
    }

    const btnEditSelectedClick = (id) => {
        navigate('/EditNews/id:' + id);
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
                <div className="pageTitle"> News </div>
                <button className="btnAddChange" onClick={() => btnAddClick}>Add</button>
                <table>
                    <thead>
                        <tr>
                            <th>Action</th>
                            <th>Title</th>
                            <th>Text</th>
                            <th>Date</th>
                            <th>Positive Rating</th>
                            <th>News Status</th>

                        </tr>
                    </thead>
                    <tbody>
                        {props.value.news.value.map(x => (
                            <tr key={x.id}>
                                <td>
                                    <button className="btnAddChange" onClick={() => btnEditSelectedClick(x.id.value)}>Edit</button>
                                </td>
                                <td>{x.title.value}</td>
                                <td>{x.text.value}</td>
                                <td>{x.date.value}</td>
                                <td>{x.positiveRating.value}</td>
                                <td>{x.newsStatus.value}</td>
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

    value: state.news,
});

export default connect(mapStateToProps, mapDispatchToProps)(News);