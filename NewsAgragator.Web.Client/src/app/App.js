import './App.css';
import React from 'react';
import { BrowserRouter,Routes, Route } from 'react-router-dom';
import { Provider } from 'react-redux';

import store from '../store';

import EditAccountStatus from './pages/AdminPages/AccountPages/EditAccountStatus/EditAccountStatus';
import EditNewsStatus from './pages/AdminPages/NewsPages/EditNewsStatus/EditNewsStatus';
import EditWeatherStatus from './pages/AdminPages/WeatherPages/EditWeatherStatus/EditWeatherStatus';

import EditPolicy from './pages/AdminPages/AccountPages/EditPolicy/EditPolicy';
import EditComplaintStatus from './pages/AdminPages/ComplaintPages/EditComplaintStatus/EditComplaintStatus';
import EditComplaintType from './pages/AdminPages/ComplaintPages/EditComplaintType/EditComplaintType';

import EditAccount from './pages/AdminPages/AccountPages/EditAccount/EditAccount';

import AccountStatuses from './pages/AdminPages/AccountPages/AccountStatuses/AccountStatuses';
import NewsStatuses from './pages/AdminPages/NewsPages/NewsStatuses/NewsStatuses';
import WeatherStatuses from './pages/AdminPages/WeatherPages/WeatherStatuses/WeatherStatuses';
import ComplaintStatuses from './pages/AdminPages/ComplaintPages/ComplaintStatuses/ComplaintStatuses';
import ComplaintTypes from './pages/AdminPages/ComplaintPages/ComplaintTypes/ComplaintTypes';
import Policies from './pages/AdminPages/AccountPages/Policies/Policies';

import Accounts from './pages/AdminPages/AccountPages/Accounts/Accounts';
import News from './pages/AdminPages/NewsPages/News/News';
import Weathers from './pages/AdminPages/WeatherPages/Weathers/Weathers';
import Comments from './pages/AdminPages/NewsPages/Comments/Comments';
import Complaints from './pages/AdminPages/ComplaintPages/Complaints/Complaints';
import NotificationMessages from './pages/AdminPages/ComplaintPages/NotificationMessages/NotificationMessages';

import EditNews from './pages/AdminPages/NewsPages/EditNews/EditNews';
import EditWeather from './pages/AdminPages/WeatherPages/EditWeather/EditWeather';
import EditComment from './pages/AdminPages/NewsPages/EditComment/EditComment';
import EditComplaint from './pages/AdminPages/ComplaintPages/EditComplaint/EditComplaint';
import EditNotificationMessage from './pages/AdminPages/ComplaintPages/EditNotificationMessage/EditNotificationMessage';

import ReferenceBooks from './pages/AdminPages/ReferenceBooks/ReferenceBooks';

// import Main from '../app/pages/MainPage/Main';

function App() {
  return (
    <Provider store={store}>
        <BrowserRouter> 
            <Routes>
                  <Route path="/" Component={ReferenceBooks} exact/>
                <Route path="/ReferenceBooks" Component={ReferenceBooks} exact />

                  <Route path="/EditWeatherStatus/" Component={EditWeatherStatus} exact />
                  <Route path="/EditWeatherStatus/:id" Component={EditWeatherStatus} exact />
                  <Route path="/WeatherStatuses" Component={WeatherStatuses} exact />

                  <Route path="/AccountStatuses" Component={AccountStatuses} exact />
                  <Route path="/EditAccountStatus/" Component={EditAccountStatus} exact />
                  <Route path="/EditAccountStatus/:id" Component={EditAccountStatus} exact />

                  <Route path="/EditNewsStatus/" Component={EditNewsStatus} exact />
                  <Route path="/EditNewsStatus/:id" Component={EditNewsStatus} exact /> 
                  <Route path="/NewsStatuses" Component={NewsStatuses} exact />

                  <Route path="/EditPolicy/" Component={EditPolicy} exact />
                  <Route path="/EditPolicy/:id" Component={EditPolicy} exact />
                  <Route path="/Policies" Component={Policies} exact />

                  <Route path="/EditComplaintStatus/" Component={EditComplaintStatus} exact />
                  <Route path="/EditComplaintStatus/:id" Component={EditComplaintStatus} exact />
                  <Route path="/ComplaintStatuses" Component={ComplaintStatuses} exact />

                  <Route path="/EditComplaintType/" Component={EditComplaintType} exact />
                  <Route path="/EditComplaintType/:id" Component={EditComplaintType} exact />
                  <Route path="/ComplaintTypes" Component={ComplaintTypes} exact />


                  <Route path="/EditAccount/" Component={EditAccount} exact />
                  <Route path="/EditAccount/:id" Component={EditAccount} exact />
                  <Route path="/Accounts" Component={Accounts} exact />

                  <Route path="/EditNews/" Component={EditNews} exact />
                  <Route path="/EditNews/:id" Component={EditNews} exact />
                  <Route path="/News" Component={News} exact />

                  <Route path="/EditWeather/" Component={EditWeather} exact />
                  <Route path="/EditWeather/:id" Component={EditWeather} exact />
                  <Route path="/Weathers" Component={Weathers} exact />

                  <Route path="/EditComment/" Component={EditComment} exact />
                  <Route path="/EditComment/:id" Component={EditComment} exact />
                  <Route path="/Comments" Component={Comments} exact />

                  <Route path="/EditComplaint/" Component={EditComplaint} exact />
                  <Route path="/EditComplaint/:id" Component={EditComplaint} exact />
                  <Route path="/Complaints" Component={Complaints} exact />

                  <Route path="/EditNotificationMessage/" Component={EditNotificationMessage} exact />
                  <Route path="/EditNotificationMessage/:id" Component={EditNotificationMessage} exact />
                  <Route path="/NotificationMessages" Component={NotificationMessages} exact />
                 
            </Routes>
        </BrowserRouter>
    </Provider>
);
}
  

export default App;
