import React, { useEffect, useLayoutEffect, useState } from 'react';
import { connect } from 'react-redux';
import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { add, save, select, clearState, loadData } from './actions';
import '../../../AdminPages/EditPage.css';


const EditWeatherStatus = (props) => {

    const navigate = useNavigate();
    const params = useParams();
    const [state, setValue] = useState({ AddOrChange: "", Loading: true, });

    const addORchangeBtn = () => {
        if (state.AddOrChange == "Add")
            return (<button className="btnAddChange" onClick={() => addWeatherStatus()}>Add</button>);
        else if (state.AddOrChange == "Change")
            return (<button className="btnAddChange" onClick={() => changeWeatherStatus()}>Change</button>);

    }

    const goToList = () => {
        navigate("/WeatherStatuses");
    }

    const changeWeatherStatus = () => {
        let data = Object.fromEntries(Object.entries(props.value.editWeatherStatus).map(e => [e[0], e[1].value]));

        let formData = new FormData();
        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }
        formData.append('id', params.id);

        props.save(formData);
    }

    const addWeatherStatus = () => {
        let data = Object.fromEntries(Object.entries(props.value.editWeatherStatus).map(e => [e[0], e[1].value]));

        let formData = new FormData();

        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }

        formData.append('id', null);

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
            props.loadData(params.id);
            console.log(params.id);
            console.log(state.AddOrChange);

        }
        else {
            setValue({ AddOrChange: "Add", Loading: true });
            props.clearState();
            console.log(state.AddOrChange);

        }
    }

    useEffect(() => {
        console.log("propsLoading changed");
        if (params.id != null) {
            setValue({ AddOrChange: "Change", Loading: props.value.editWeatherStatus.loading });
        }
        else if (params.id == null) {
            setValue({ AddOrChange: "Add", Loading: props.value.editWeatherStatus.loading });
        }
        console.log(state.AddOrChange);
    }, [props.value.editWeatherStatus.loading]);

    useLayoutEffect(() => {
        beforeRender();
    }, []);


    console.log(props);
    return (
        console.log(props),
        <Wrapper>
            <div className="editPage">
                <div className='pageTitle'> Edit Weather Status</div>
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
