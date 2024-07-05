import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { select, add, save, loadParameters, loadData, clearState } from './actions';
import InputObject from '../../../../customComponents/InputObject/InputObject';
import '../../../AdminPages/EditPage.css';


const EditComplaint = (props) => {

    const navigate = useNavigate();

    const addORchangeBtn = (addORchange) => {
        if (addORchange == "Add")
            return (<button className="btnAddChange" onClick={() => addComplaint}>Add</button>);
        else if (addORchange == "Change")
            return (<button className="btnAddChange" onClick={() => changeComplaint}>Change</button>);

    }

    const changeComplaint = () => {
        const data = Object.fromEntries(Object.entries(props.value.editComplaint).map(([key, value]) => [key, value.value]));

        const formData = new FormData();

        for (const key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        props.change(formData);
        navigate("/Complaints");
    }

    const addComplaint = () => {
        const data = Object.fromEntries(Object.entries(props.value.editComplaint).map(([key, value]) => [key, value.value]));

        const formData = new FormData();

        for (const key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        formData.delete('id');
        formData.append('id', null);

        props.add(formData);
        navigate("/Complaints");
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
                <div className="pageTitle">Edit Complaint </div>
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
                            value={props.value.editComplaint.comment.value}
                            options={props.value.editComplaint.comment.options}
                            placeholder="Comment"
                            onClick={(val) => this.props.select('comment', val)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">News</div>
                        <InputObject
                            id="news"
                            value={props.value.editComplaint.news.value}
                            options={props.value.editComplaint.news.options}
                            placeholder="News"
                            onClick={(val) => this.props.select('news', val)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Complaint Status</div>
                        <InputObject
                            id="compalintStatus"
                            value={props.value.editComplaint.compalintStatus.value}
                            options={props.value.editComplaint.compalintStatus.options}
                            placeholder="Compalint Status"
                            onClick={(val) => this.props.select('compalintStatus', val)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Complaint Type</div>
                        <InputObject
                            id="compalintType"
                            value={props.value.editComplaint.compalintType.value}
                            options={props.value.editComplaint.compalintType.options}
                            placeholder="Compalint Type"
                            onClick={(val) => this.props.select('compalintType', val)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">User</div>
                        <InputObject
                            id="user"
                            value={props.value.editComplaint.user.value}
                            options={props.value.editComplaint.user.options}
                            placeholder="User"
                            onClick={(val) => this.props.select('user', val)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Administrator</div>
                        <InputObject
                            id="administrator"
                            value={props.value.editComplaint.administrator.value}
                            options={props.value.editComplaint.administrator.options}
                            placeholder="Administrator"
                            onClick={(val) => this.props.select('administrator', val)}
                        />
                    </div>
                </div>
                <button className="btnAddChange" onClick={() => addComplaint}>Add</button>
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
        add: (newComplaint) => dispatch(add(newComplaint)),
        save: (updatedComplaint) => dispatch(save(updatedComplaint)),
        loadParameters: () => dispatch(loadParameters()),
        loadData: (id) => dispatch(loadData(id)),
        clearState: (data) => dispatch(clearState(data)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.editComplaint,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditComplaint);