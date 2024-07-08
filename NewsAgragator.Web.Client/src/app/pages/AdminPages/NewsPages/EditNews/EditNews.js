import React, { useEffect, useLayoutEffect, useState } from 'react';
import { connect } from 'react-redux';
import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { add, save, select, selectParameter, clearState, load, loadParameters } from './actions';
import InputObject from '../../../../customComponents/InputObject/InputObject';
import '../../../AdminPages/EditPage.css';


const EditNews = (props) => {

    const navigate = useNavigate();
    const params = useParams();
    const [state, setValue] = useState({ AddOrChange: "", Loading: true, });

    const addORchangeBtn = () => {
        if (state.AddOrChange == "Add")
            return (<button className="btnAddChange" onClick={() => addNews()}>Add</button>);
        else if (state.AddOrChange == "Change")
            return (<button className="btnAddChange" onClick={() => changeNews()}>Change</button>);

    }

    const goToList = () => {
        navigate("/News");
    }

    const changeNews = () => {
        let data = Object.fromEntries(Object.entries(props.value.editNews).map(e => [e[0], e[1].value]));
        data.positiveRating = Number.parseInt(data.positiveRating);
        data.newsStatusId = data.newsStatus.id;
        data.id = params.id;

        let formData = new FormData();

        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }
        //formData.append('id', params.id);
        //formData.append('newsStatusId', props.value.editNews.newsStatus.value.id);

        props.save(formData);
    }

    const addNews = () => {
        let data = Object.fromEntries(Object.entries(props.value.editNews).map(e => [e[0], e[1].value]));
        data.positiveRating = Number.parseInt(data.positiveRating);
        data.newsStatusId = data.newsStatus.id;
        data.id = null;

        let formData = new FormData();

        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        //formData.append('newsStatusId', props.value.editNews.newsStatus.value.id);
        //formData.append('id', null);

        props.add(formData);
    };

    const renderInputs = () => {
        console.log("renderInputs");

        if (state.Loading == false) {

            return (
                <div className="editPageInputs">
                    <div className="divInput">
                        <div className="inputTitle">Title</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Title"
                            value={props.value.editNews.title.value}
                            onChange={(e) => props.select("title", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Text</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Text"
                            value={props.value.editNews.text.value}
                            onChange={(e) => props.select("text", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Date</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Date"
                            value={props.value.editNews.date.value}
                            onChange={(e) => props.select("date", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Positive Rating</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Positive Rating"
                            value={props.value.editNews.positiveRating.value}
                            onChange={(e) => props.select("positiveRating", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">News Status</div>
                        <InputObject
                            id="newsStatus"
                            value={props.value.editNews.newsStatus.value.id}
                            options={props.value.editNews.newsStatuses.value}
                            placeholder="News Status"
                            onClick={(val, text) => props.selectParameter('newsStatus', val, text)}
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
            setValue({ AddOrChange: "Change", Loading: props.value.editNews.loading });
        }
        else if (params.id == null) {
            setValue({ AddOrChange: "Add", Loading: props.value.editNews.loading });
        }
        console.log(state.AddOrChange);
    }, [props.value.editNews.loading]);

    useLayoutEffect(() => {
        beforeRender();
    }, []);

    console.log(props);
    return (

        <Wrapper>
            <div className="editPage">
                <div className="pageTitle">Edit News </div>

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
        add: (newNews) => dispatch(add(newNews)),
        save: (updatedNews) => dispatch(save(updatedNews)),
        loadParameters: () => dispatch(loadParameters()),
        load: (id) => dispatch(load(id)),
        clearState: (data) => dispatch(clearState(data)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.editNews,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditNews);