import { createStore, combineReducers, applyMiddleware } from 'redux';
import * as reducers from './reducers';
import apiMiddleware from './apiMiddleware';
import authorizationMiddleware from './app/pages/ClientPages/AccountPages/Authorization/middleware';
import initialState from './initialState';

// const store = createStore(combineReducers(reducers), applyMiddleware(apiMiddleware));

const store = createStore(combineReducers(reducers), applyMiddleware(authorizationMiddleware, apiMiddleware));

store.subscribe(() => {
    console.log(store.getState());
});

export default store;