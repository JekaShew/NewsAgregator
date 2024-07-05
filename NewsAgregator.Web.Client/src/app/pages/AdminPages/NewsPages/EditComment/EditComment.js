import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { select, add, save, loadParameters, loadData, clearState } from './actions';
import InputObject from '../../../../customComponents/InputObject/InputObject';
import '../../../AdminPages/EditPage.css';


const EditComment = (props) => {

    const navigate = useNavigate();

    const addORchangeBtn = (addORchange) => {
        if (addORchange == "Add")
            return (<button className="btnAddChange" onClick={() => addComment}>Add</button>);
        else if (addORchange == "Change")
            return (<button className="btnAddChange" onClick={() => changeComment}>Change</button>);

    }

    const changeComment = () => {
        const data = Object.fromEntries(Object.entries(props.value.editComment).map(([key, value]) => [key, value.value]));

        const formData = new FormData();

        for (const key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        props.change(formData);
        navigate("/Comments");
    }

    const addComment = () => {
        const data = Object.fromEntries(Object.entries(props.value.editComment).map(([key, value]) => [key, value.value]));

        const formData = new FormData();

        for (const key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        formData.delete('id');
        formData.append('id', null);

        props.add(formData);
        navigate("/Comments");
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
                <div className="pageTitle">Edit Comment </div>
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
                            value={props.value.editComment.account.value}
                            options={props.value.editComment.account.options}
                            placeholder="Account"
                            onClick={(val) => this.props.select('account', val)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">News</div>
                        <InputObject
                            id="news"
                            value={props.value.editComment.news.value}
                            options={props.value.editComment.news.options}
                            placeholder="News"
                            onClick={(val) => this.props.select('news', val)}
                        />
                    </div>
                </div>
                <button className="btnAddChange" onClick={() => addComment}>Add</button>
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
        add: (newComment) => dispatch(add(newComment)),
        save: (updatedComment) => dispatch(save(updatedComment)),
        loadParameters: () => dispatch(loadParameters()),
        loadData: (id) => dispatch(loadData(id)),
        clearState: (data) => dispatch(clearState(data)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.editComment,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditComment);