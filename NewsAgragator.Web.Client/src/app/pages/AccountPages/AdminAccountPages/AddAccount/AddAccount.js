import React from 'react';
import { connect } from 'react-redux';
import Wrapper from '../../../Wrapper/Wrapper';
import InputObject from '../../../../customComponents/InputObject/InputObject';
import { loadParameters, select } from './actions';

var ReactRouterDom = require('react-router-dom');

const NavLink = ReactRouterDom.NavLink;
const withRouter = ReactRouterDom.withRouter;


class Account extends React.Component {
    constructor(props) {
        super(props);
        this.props.loadParameters();
    }

    render() {
        return (
            <Wrapper>
                <div className="account">

                    <div>
                        <div className="">

                            <div className="">
                                <div className="">Username</div>
                                <input
                                    className=""
                                    type="text"
                                    placeholder="Username"
                                    value={this.props.value.userName.value}
                                    onChange={(e) => this.props.select("userName", e.target.value)}
                                >
                                </input>
                            </div>
                            <div className="">
                                <div className="">FIO</div>
                                <input
                                    className=""
                                    type="text"
                                    placeholder="FIO"
                                    value={this.props.value.fIO.value}
                                    onChange={(e) => this.props.select("fIO", e.target.value)}
                                >
                                </input>
                            </div>
                            <div className="">
                                <div className="">Email</div>
                                <input
                                    className=""
                                    type="text"
                                    placeholder="Email"
                                    value={this.props.value.email.value}
                                    onChange={(e) => this.props.select("email", e.target.value)}
                                >
                                </input>
                            </div>
                            <div className="">
                                <div className="">Phone</div>
                                <input
                                    className=""
                                    type="text"
                                    placeholder="Phone"
                                    value={this.props.value.phone.value}
                                    onChange={(e) => this.props.select("phone", e.target.value)}
                                >
                                </input>
                            </div>
                            <div className="">
                                <div className="">Desired News Rating</div>
                                <input
                                    className=""
                                    type="number"
                                    placeholder="Desired News Rating"
                                    value={this.props.value.desiredNewsRating.value}
                                    onChange={(e) => this.props.select("desiredNewsRating", e.target.value)}
                                >
                                </input>
                            </div>
                            <div className="">
                                <div className="">Login</div>
                                <input
                                    className=""
                                    type="text"
                                    placeholder="Login"
                                    value={this.props.value.login.value}
                                    onChange={(e) => this.props.select("login", e.target.value)}
                                >
                                </input>
                            </div>
                            <div className="">
                                <div className="">Password</div>
                                <input
                                    className=""
                                    type="text"
                                    placeholder="Password"
                                    value={this.props.value.password.value}
                                    onChange={(e) => this.props.select("password", e.target.value)}
                                >
                                </input>
                            </div>
                            <div className="">
                                <div className="">Repeat Password</div>
                                <input
                                    className=""
                                    type="text"
                                    placeholder="Repeat Password"
                                    value={this.props.value.repeatPassword.value}
                                    onChange={(e) => this.props.select("repeatPassword", e.target.value)}
                                >
                                </input>
                            </div>
                            
                        </div>
                        <div>
                            <div className="">
                                <div className="">Account Status</div>
                                <InputObject
                                    id="accountStatus"
                                    value={this.props.value.accountStatus.value}
                                    options={this.props.value.accountStatus.options}
                                    placeholder="Account Status"
                                    onClick={(val) => this.props.select('accountStatus', val)}
                                />
                            </div>
                        </div>
                        <div>
                            <div className="">
                                <div className="">Account Role</div>
                                <InputObject
                                    id="accountRole"
                                    value={this.props.value.accountRole.value}
                                    options={this.props.value.accountRole.options}
                                    placeholder="Account Role"
                                    onClick={(val) => this.props.select('accountRole', val)}
                                />
                            </div>
                        </div>
                    </div>
                </div>
            </Wrapper>
        )


    };
}

const mapDispatchToProps = dispatch => {
    return {
        loadParameters: () => dispatch(loadParameters()),
        select: (name, value) => dispatch(select(name, value)),
    }
}

function mapStateToProps(state) {
    return {

        value: state.account,
    };
}
export default connect(mapStateToProps, mapDispatchToProps)(withRouter(Account));
