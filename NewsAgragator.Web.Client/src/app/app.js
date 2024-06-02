import React, { Component } from 'react';
import { BrowserRouter as RoutingRouter, Route as RoutingRoute } from 'react-router-dom';
import { Provider } from 'react-redux';

import AddAccountStatus from '../app/pages/AccountPages/AdminAccountPages/AddAccountStatus/AddAccountStatus';
import Main from '../app/pages/MainPage/Main';


export default class extends Component {
    render() {
        return this.prod();
    }

    prod() {
        return (
            <Provider store={this.props.store}>
                <RoutingRouter>
                    <RoutingRoute path="/" component={Main} exact />
                    <RoutingRoute path="/addaccountstatus" component={AddAccountStatus} exact />



                </RoutingRouter>
            </Provider>
        );
    }
}
