import React, { useEffect, useLayoutEffect, useState } from 'react';
import { connect } from 'react-redux';
import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { add, save, select, selectParameter, clearState, load, loadParameters } from './actions';
import InputObject from '../../../../customComponents/InputObject/InputObject';
import '../../../AdminPages/EditPage.css';


const EditComment = (props) => {

    const navigate = useNavigate();
    const params = useParams();
    const [state, setValue] = useState({ AddOrChange: "", Loading: true, });

    const addORchangeBtn = () => {
        if (state.AddOrChange == "Add")
            return (<button className="btnAddChange" onClick={() => addComment()}>Add</button>);
        else if (state.AddOrChange == "Change")
            return (<button className="btnAddChange" onClick={() => changeComment()}>Change</button>);

    }

    const goToList = () => {
        navigate("/Comments");
    }

    const changeComment = () => {
        let data = Object.fromEntries(Object.entries(props.value.editComment).map(e => [e[0], e[1].value]));
        data.newsId = data.news.id;
        data.accountId = data.account.id;
        data.id = params.id;


        let formData = new FormData();

        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }
        //formData.append('id', params.id);
        //formData.append('accountId', props.value.editComment.account.value.id);
        //formData.append('newsId', props.value.editComment.news.value.id);

        props.save(formData);
    }

    const addComment = () => {
        let data = Object.fromEntries(Object.entries(props.value.editComment).map(e => [e[0], e[1].value]));
        data.newsId = data.news.id;
        data.accountId = data.account.id;
        data.id = null;
        console.log("AddComment");
        console.log(data)
        let formData = new FormData();

        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }
        //formData.append('accountId', props.value.editComment.account.value.id);
        //formData.append('newsId', props.value.editComment.news.value.id);
        //formData.append('id', null);

        props.add(formData);
    };

    const renderInputs = () => {
        console.log("renderInputs");

        if (state.Loading == false) {
            return (
                <div className="editPageInputs">
                    <div className="divInput">
                        <div className="inputTitle">Text</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Text"
                            value={props.value.editComment.text.value}
                            onChange={(e) => props.select("text", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Date</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Date"
                            value={props.value.editComment.date.value}
                            onChange={(e) => props.select("date", e.target.value)}
                        />
                    </div>

                    <div className="divInput">
                        <div className="inputTitle">Account </div>
                        <InputObject
                            id="account"
                            value={props.value.editComment.account.value.id}
                            options={props.value.editComment.accounts.value}
                            placeholder="Account"
                            onClick={(val, text) => props.selectParameter('account', val, text)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">News</div>
                        <InputObject
                            id="news"
                            value={props.value.editComment.news.value.id}
                            options={props.value.editComment.newses.value}
                            placeholder="News"
                            onClick={(val, text) => props.selectParameter('news', val, text)}
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
            setValue({ AddOrChange: "Change", Loading: props.value.editComment.loading });
        }
        else if (params.id == null) {
            setValue({ AddOrChange: "Add", Loading: props.value.editComment.loading });
        }
        console.log(state.AddOrChange);
    }, [props.value.editComment.loading]);

    useLayoutEffect(() => {
        beforeRender();
    }, []);

    console.log(props);
    return (

        <Wrapper>
            <div className="editPage">
                <div className="pageTitle">Edit Comment </div>
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
        add: (newComment) => dispatch(add(newComment)),
        save: (updatedComment) => dispatch(save(updatedComment)),
        loadParameters: () => dispatch(loadParameters()),
        load: (id) => dispatch(load(id)),
        clearState: (data) => dispatch(clearState(data)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.editComment,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditComment);