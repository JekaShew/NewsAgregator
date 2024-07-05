import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { select, add, save, clearState, loadData } from './actions';
import '../../../AdminPages/EditPage.css';


const EditWeatherStatus = (props) => {

    const navigate = useNavigate();

    const addORchangeBtn = (addORchange) => {
        if (addORchange == "Add")
            return (<button className="btnAddChange" onClick={() => addWeatherStatus}>Add</button>);
        else if (addORchange == "Change")
            return (<button className="btnAddChange" onClick={() => changeWeatherStatus}>Change</button>);

    }

    const changeWeatherStatus = () => {
        const data = Object.fromEntries(Object.entries(props.value.editWeatherStatus).map(([key, value]) => [key, value.value]));

        const formData = new FormData();

        for (const key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        props.save(formData);
        navigate("/WeatherStatuses");
    }

    const addWeatherStatus = () => {
        const data = Object.fromEntries(Object.entries(props.value.editWeatherStatus).map(([key, value]) => [key, value.value]));

        const formData = new FormData();

        for (const key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        formData.delete('id');
        formData.append('id', null);

        props.add(formData);
        navigate("/WeatherStatuses");
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
        console.log(props),
        <Wrapper>
            <div className="editPage">
                <div className='pageTitle'> Edit Weather Status</div>
                <div className="editPageInputs">
                    <div className="divInput">
                        <div className="inputTitle">Title</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Title"
                            value={props.value.editWeatherStatus.title.value}
                            onChange={(e) => props.select("title", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Description</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Description"
                            value={props.value.editWeatherStatus.description.value}
                            onChange={(e) => props.select("description", e.target.value)}
                        />
                    </div>
                </div>
                <button className="btnAddChange" onClick={() => addWeatherStatus}>Add</button>
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
        add: (newWeatherStatus) => dispatch(add(newWeatherStatus)),
        save: (updatedWeatherStatus) => dispatch(save(updatedWeatherStatus)),
        clearState: (data) => dispatch(clearState(data)),
        loadData: (id) => dispatch(loadData(id)),
    }
 }

const mapStateToProps = (state) => (console.log("mapStateToProps", state), { 

    value: state.editWeatherStatus,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditWeatherStatus);
