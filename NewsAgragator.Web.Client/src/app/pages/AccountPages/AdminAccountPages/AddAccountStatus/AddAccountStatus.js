import React from 'react';
import { connect } from 'react-redux';
import Wrapper from '../../../Wrapper/Wrapper';
import { select, add } from './actions';

var ReactRouterDom = require('react-router-dom');

const NavLink = ReactRouterDom.NavLink;
const withRouter = ReactRouterDom.withRouter;


class AccountStatus extends React.Component {
    constructor(props) {
        super(props);
        this.addAccount = this.addAccount.bind(this);
    }


    addAccount() {

        let data = Object.fromEntries(Object.entries(this.props.value.accountStatus).map(e => [e[0], e[1].value]));
   
        let formData = new FormData();

        for (var key in data)
            if (data[key])
                formData.append(key, data[key]);

        formData.delete('id');
        formData.append('id', null);

        this.props.add(formData);
        this.props.history.push("/AccountStatuses");
    }


    render() {
        return (
            <Wrapper>
                <div className="accountStatus">
                    <div>
                        <div className="">
                            <div className="">Title</div>
                            <input
                                className=""
                                type="text"
                                placeholder="Title"
                                value={this.props.value.title.value}
                                onChange={(e) => this.props.select("title", e.target.value)}
                            >
                            </input>
                        </div>
                        <div className="">
                            <div className="">Description</div>
                            <input
                                className=""
                                type="text"
                                placeholder="Description"
                                value={this.props.value.description.value}
                                onChange={(e) => this.props.select("description", e.target.value)}
                            >
                            </input>
                        </div>
                          
                    </div>
                    <button className="btnSaveDetails" onClick={() => this.addAccount()} > Add </button>)
                </div>
            </Wrapper>
        )

    };
}

const mapDispatchToProps = dispatch => {
    return {
        select: (name, value) => dispatch(select(name, value)),
        add: (newAccountStatus) => dispatch(add(newAccountStatus)),
    }
}

function mapStateToProps(state) {
    return {

        value: state.account,
    };
}
export default connect(mapStateToProps, mapDispatchToProps)(AccountStatus);
