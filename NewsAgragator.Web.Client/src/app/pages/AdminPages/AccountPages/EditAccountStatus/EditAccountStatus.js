import React, { useEffect, useLayoutEffect, useState } from 'react';
import { connect } from 'react-redux';
import { useParams } from 'react-router';
import { useNavigate } from 'react-router-dom';
import Wrapper from '../../../Wrapper/Wrapper';
import { add, save, select, clearState, loadData } from './actions';
import '../../../AdminPages/EditPage.css';


const EditAccountStatus = (props) => {
    const navigate = useNavigate();
    const params = useParams();
    const [state, setValue] = useState({ AddOrChange: "", Loading: true, });

    const addORchangeBtn = () => {
        if (state.AddOrChange == "Add") 
            return (<button className="btnAddChange" onClick={() => addAccountStatus()}>Add</button>);
        else if (state.AddOrChange == "Change")
            return (<button className="btnAddChange" onClick={() => changeAccountStatus()}>Change</button>);
        
    }

    const goToList = () => {
        navigate("/AccountStatuses");
    }
    const changeAccountStatus = () => {
        let data = Object.fromEntries(Object.entries(props.value.editAccountStatus).map(e => [e[0], e[1].value]));

        let formData = new FormData();
        for (var key in data) {
            if (data[key]) {
                formData.append(key, data[key]);
            }
        }
        formData.append('id', params.id);

        props.save(formData);
        
    }

    const addAccountStatus = () => {
        let data = Object.fromEntries(Object.entries(props.value.editAccountStatus).map(e => [e[0], e[1].value]));

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
                            value={props.value.editAccountStatus.title.value}
                            onChange={(e) => props.select("title", e.target.value)}
                        />
                    </div>
                    <div className="divInput">
                        <div className="inputTitle">Description</div>
                        <input
                            className="input"
                            type="text"
                            placeholder="Description"
                            value={props.value.editAccountStatus.description.value}
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
            setValue({ AddOrChange: "Change", Loading: props.value.editAccountStatus.loading });
        }
        else if (params.id == null) {
            setValue({ AddOrChange: "Add", Loading: props.value.editAccountStatus.loading });
        }
        console.log(state.AddOrChange);
    }, [props.value.editAccountStatus.loading]);

    useLayoutEffect(() => {
        beforeRender();
    }, []);

    //const beforeRender = () =>
    //{
    //    console.log("BeforeRenderEditAccountStatus");
    //    console.log(params);
    //    console.log(params.id);
    //    if (params.id != null) {
    //        props.loadData(params.id);
    //        setValue("Change");
    //    }
    //    else {
    //        props.clearState();
    //        setValue("Add");
    //    }
    //}


    console.log(props);
    return (
        
        <Wrapper>
            <div className="editPage">
                <div className="pageTitle">Edit Account Status </div>
                
                { renderInputs() }
               
                <div className="btns">
                    <button className="btnAddChange" onClick={() => goToList()}>Back to List</button>
                    <div>
                        { addORchangeBtn() }
                    </div>
                </div>
            </div>
        </Wrapper>
    );
};

const mapDispatchToProps = dispatch => {
    return {
        select: (name, value) => dispatch(select(name, value)),
        add: (newAccountStatus) => dispatch(add(newAccountStatus)),
        save: (updatedAccountStatus) => dispatch(save(updatedAccountStatus)),
        clearState: (data) => dispatch(clearState(data)),
        loadData: (id) => dispatch(loadData(id)),
    }
 }

const mapStateToProps = (state) => ( console.log("mapStateToProps"),{
    
    value: state.editAccountStatus,
});

export default connect(mapStateToProps, mapDispatchToProps)(EditAccountStatus);



// import React from 'react';
// import { connect } from 'react-redux';
// import Wrapper from '../../../Wrapper/Wrapper';
// import { select, add } from './actions';

// var ReactRouterDom = require('react-router-dom');

// const NavLink = ReactRouterDom.NavLink;
// const withRouter = ReactRouterDom.withRouter;


// class AddAccountStatus extends React.Component {
//    constructor(props) {
//        super(props);
//        this.addAccount = this.addAccount.bind(this);
//    }


//    addAccount() {

//        let data = Object.fromEntries(Object.entries(this.props.value.accountStatus).map(e => [e[0], e[1].value]));
   
//        let formData = new FormData();

//        for (var key in data)
//            if (data[key])
//                formData.append(key, data[key]);

//        formData.delete('id');
//        formData.append('id', null);

//        this.props.add(formData);
//        this.props.history.push("/AccountStatuses");
//    }


//    render() {
//     console.log(this.props);
//        return (
//            <Wrapper>
//                <div className="accountStatus">
//                    <div>
//                        <div className="">
//                            <div className="">Title</div>
//                            <input
//                                className=""
//                                type="text"
//                                placeholder="Title"
//                                value={this.props.value.title.value}
//                                onChange={(e) => this.props.select("title", e.target.value)}
//                            >
//                            </input>
//                        </div>
//                        <div className="">
//                            <div className="">Description</div>
//                            <input
//                                className=""
//                                type="text"
//                                placeholder="Description"
//                                value={this.props.value.description.value}
//                                onChange={(e) => this.props.select("description", e.target.value)}
//                            >
//                            </input>
//                        </div>
                          
//                    </div>
//                    <button className="btnSaveDetails" onClick={() => this.addAccount()} > Add </button>)
//                </div>
//            </Wrapper>
//        )

//    };
// }

// const mapDispatchToProps = dispatch => {
//    return {
//        select: (name, value) => dispatch(select(name, value)),
//        add: (newAccountStatus) => dispatch(add(newAccountStatus)),
//    }
// }

// function mapStateToProps(state) {
//    return {

//        value: state.accountStatuses,
//    };
// }
// export default connect(mapStateToProps, mapDispatchToProps)(AddAccountStatus);
