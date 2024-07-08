import React, { useEffect, useLayoutEffect, useState } from 'react';
import { connect } from 'react-redux';
import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { add, save, select, selectParameter, clearState, load, loadParameters } from './actions';
import InputObject from '../../../../customComponents/InputObject/InputObject';
import '../../../AdminPages/EditPage.css';


const EditComplaint = (props) => {

    const navigate = useNavigate();
    const params = useParams();
    const [state, setValue] = useState({ AddOrChange: "", Loading: true, });

    const addORchangeBtn = () => {
        if (state.AddOrChange == "Add")
            return (<button className="btnAddChange" onClick={() => addComplaint()}>Add</button>);
        else if (state.AddOrChange == "Change")
            return (<button className="btnAddChange" onClick={() => changeComplaint()}>Change</button>);

    }

    const goToList = () => {
        navigate("/Complaints");
    }

    const changeComplaint = () => {
        let data = Object.fromEntries(Object.entries(props.value.editComplaint).map(e => [e[0], e[1].value]));
        data.commentId = data.comment.id;
        data.newsId = data.news.id;
        data.complaintStatusId = data.complaintStatus.id;
        data.complaintTypeId = data.complaintType.id;
        data.userId = data.user.id;
        data.administratorId = data.administrator.id;
        data.id = params.id;;


        let formData = new FormData();

        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }
        //formData.append('id', params.id);
        //formData.append('commentId', props.value.editComplaint.comment.value.id);
        //formData.append('newsId', props.value.editComplaint.news.value.id);
        //formData.append('complaintStatusId', props.value.editComplaint.complaintStatus.value.id);
        //formData.append('complaintTypeId', props.value.editComplaint.complaintType.value.id);
        //formData.append('administratorId', props.value.editComplaint.administrator.value.id);
        //formData.append('userId', props.value.editComplaint.user.value.id);

        props.save(formData);
    }

    const addComplaint = () => {
        let data = Object.fromEntries(Object.entries(props.value.editComplaint).map(e => [e[0], e[1].value]));
        data.commentId = data.comment.id;
        data.newsId = data.news.id;
        data.complaintStatusId = data.complaintStatus.id;
        data.complaintTypeId = data.complaintType.id;
        data.userId = data.user.id;
        data.administratorId = data.administrator.id;
        data.id = null;


        let formData = new FormData();

        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        //formData.append('commentId', props.value.editComplaint.comment.value.id);
        //formData.append('newsId', props.value.editComplaint.news.value.id);
        //formData.append('complaintStatusId', props.value.editComplaint.complaintStatus.value.id);
        //formData.append('complaintTypeId', props.value.editComplaint.complaintType.value.id);
        //formData.append('administratorId', props.value.editComplaint.administrator.value.id);
        //formData.append('userId', props.value.editComplaint.user.value.id);
        //formData.append('id', null);

        props.add(formData);
    };

    const renderInputs = () => {
        console.log("renderInputs");
        console.log(props.value.editComplaint);
        if (state.Loading == false) {
            return (
                <div className="editPageInputs">
                    <div className="divInput">
                        <div className="inputTitle">Title</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Title"
                            value={props.value.editComplaint.title.value}
                            onChange={(e) => props.select("title", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Text</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Text"
                            value={props.value.editComplaint.text.value}
                            onChange={(e) => props.select("text", e.target.value)}
                        />
                    </div>

                    <div className="divInput">
                        <div className="inputTitle">Comment</div>
                        <InputObject
                            id="comment"
                            value={props.value.editComplaint.comment.value.id}
                            options={props.value.editComplaint.comments.value}
                            placeholder="Comment"
                            onClick={(val, text) => props.selectParameter('comment', val,text)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">News</div>
                        <InputObject
                            id="news"
                            value={props.value.editComplaint.news.value.id}
                            options={props.value.editComplaint.newses.value}
                            placeholder="News"
                            onClick={(val, text) => props.selectParameter('news', val, text)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Complaint Status</div>
                        <InputObject
                            id="compalintStatus"
                            value={props.value.editComplaint.complaintStatus.value.id}
                            options={props.value.editComplaint.complaintStatuses.value}
                            placeholder="Compalint Status"
                            onClick={(val, text) => props.selectParameter('complaintStatus', val, text)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Complaint Type</div>
                        <InputObject
                            id="complaintType"
                            value={props.value.editComplaint.complaintType.value.id}
                            options={props.value.editComplaint.complaintTypes.value}
                            placeholder="Complaint Type"
                            onClick={(val, text) => props.selectParameter('complaintType', val, text)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">User</div>
                        <InputObject
                            id="user"
                            value={props.value.editComplaint.user.value.id}
                            options={props.value.editComplaint.accounts.value}
                            placeholder="User"
                            onClick={(val, text) => props.selectParameter('user', val, text)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Administrator</div>
                        <InputObject
                            id="administrator"
                            value={props.value.editComplaint.administrator.value.id}
                            options={props.value.editComplaint.accounts.value}
                            placeholder="Administrator"
                            onClick={(val, text) => props.selectParameter('administrator', val, text)}
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
            setValue({ AddOrChange: "Change", Loading: props.value.editComplaint.loading });
        }
        else if (params.id == null) {
            setValue({ AddOrChange: "Add", Loading: props.value.editComplaint.loading });
        }
        console.log(state.AddOrChange);
    }, [props.value.editComplaint.loading]);

    useLayoutEffect(() => {
        beforeRender();
    }, []);


    /*console.log(props);*/
    return (

        <Wrapper>
            <div className="editPage">
                <div className="pageTitle">Edit Complaint </div>
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
        add: (newComplaint) => dispatch(add(newComplaint)),
        save: (updatedComplaint) => dispatch(save(updatedComplaint)),
        loadParameters: () => dispatch(loadParameters()),
        load: (id) => dispatch(load(id)),
        clearState: (data) => dispatch(clearState(data)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.editComplaint,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditComplaint);