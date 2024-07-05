import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { select, add, save, loadData, clearState } from './actions';
import '../../../AdminPages/EditPage.css';


const EditPolicy = (props) => {

    const navigate = useNavigate();

    const addORchangeBtn = (addORchange) => {
        if (addORchange == "Add")
            return (<button className="btnAddChange" onClick={() => addPolicy}>Add</button>);
        else if (addORchange == "Change")
            return (<button className="btnAddChange" onClick={() => changePolicy}>Change</button>);

    }

    const changePolicy = () => {
        const data = Object.fromEntries(Object.entries(props.value.editPolicy).map(([key, value]) => [key, value.value]));

        const formData = new FormData();

        for (const key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        props.save(formData);
        navigate("/Policies");
    }

    const addPolicy = () => {
        const data = Object.fromEntries(Object.entries(props.value.editPolicy).map(([key, value]) => [key, value.value]));

        const formData = new FormData();

        for (const key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        formData.delete('id');
        formData.append('id', null);

        props.add(formData);
        navigate("/Policies");
    };

    const beforeRender = () => {
        if (props.match.params.id) {
            props.loadData(props.match.params.id);
            addORchangeBtn("Change");
        }
        else {
            props.clearState();
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
                <div className="pageTitle"> Edit Policy </div>
                <div className="editPageInputs">
                    <div className="divInput">
                        <div className="inputTitle">Title</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Title"
                            value={props.value.editPolicy.title.value}
                            onChange={(e) => props.select("title", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Description</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Description"
                            value={props.value.editPolicy.description.value}
                            onChange={(e) => props.select("description", e.target.value)}
                        />
                    </div>
                </div>
                <button className="btnAddChange" onClick={() => addPolicy}>Add</button>
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
        add: (newPolicy) => dispatch(add(newPolicy)),
        save: (updatedPolicy) => dispatch(save(updatedPolicy)),
        clearState: (data) => dispatch(clearState(data)),
        loadData: (id) => dispatch(loadData(id)),
    }
}

const mapStateToProps = (state) => (console.log("mapStateToProps"), {

    value: state.editPolicy,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditPolicy);
