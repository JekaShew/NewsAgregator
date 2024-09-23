import editAccountStatus from './app/pages/AdminPages/AccountPages/EditAccountStatus/reducer';
import editNewsStatus from './app/pages/AdminPages/NewsPages/EditNewsStatus/reducer';
import editWeatherStatus from './app/pages/AdminPages/WeatherPages/EditWeatherStatus/reducer';

import editPolicy from './app/pages/AdminPages/AccountPages/EditPolicy/reducer';
import editComplaintStatus from './app/pages/AdminPages/ComplaintPages/EditComplaintStatus/reducer';
import editComplaintType from './app/pages/AdminPages/ComplaintPages/EditComplaintType/reducer';

import editAccount from './app/pages/AdminPages/AccountPages/EditAccount/reducer';

import accountStatuses from './app/pages/AdminPages/AccountPages/AccountStatuses/reducer';
import newsStatuses from './app/pages/AdminPages/NewsPages/NewsStatuses/reducer';
import weatherStatuses from './app/pages/AdminPages/WeatherPages/WeatherStatuses/reducer';
import complaintStatuses from './app/pages/AdminPages/ComplaintPages/ComplaintStatuses/reducer';
import complaintTypes from './app/pages/AdminPages/ComplaintPages/ComplaintTypes/reducer';
import policies from './app/pages/AdminPages/AccountPages/Policies/reducer';

import accounts from './app/pages/AdminPages/AccountPages/Accounts/reducer';
import complaints from './app/pages/AdminPages/ComplaintPages/Complaints/reducer';
import comments from './app/pages/AdminPages/NewsPages/Comments/reducer';
import news from './app/pages/AdminPages/NewsPages/News/reducer';
import weathers from './app/pages/AdminPages/WeatherPages/Weathers/reducer';
import notificationMessages from './app/pages/AdminPages/ComplaintPages/NotificationMessages/reducer';

import editComplaint from './app/pages/AdminPages/ComplaintPages/EditComplaint/reducer';
import editComment from './app/pages/AdminPages/NewsPages/EditComment/reducer';
import editNews from './app/pages/AdminPages/NewsPages/EditNews/reducer';
import editWeather from './app/pages/AdminPages/WeatherPages/EditWeather/reducer';
import editNotificationMessage from './app/pages/AdminPages/ComplaintPages/EditNotificationMessage/reducer';

import clientMain from './app/pages/ClientPages/MainPage/reducer'

import signIN from './app/pages/ClientPages/AccountPages/SignINPage/reducer'
import signUP from './app/pages/ClientPages/AccountPages/SignUPPage/reducer'
import authorization from './app/pages/ClientPages/AccountPages/Authorization/reducer'
import changePassword from './app/pages/ClientPages/AccountPages/ChangePasswordPage/reducer'
import changePasswordWhenForgot from './app/pages/ClientPages/AccountPages/ForgotPasswordPage/reducer'

const lastAction = (state = null, action) => {
    return action;
}

export {clientMain}

export {signIN}
export {signUP}
export {authorization}
export {changePassword}
export {changePasswordWhenForgot}



export { editWeatherStatus }
export { editAccountStatus }
export { editNewsStatus }

export { editPolicy }
export { editComplaintStatus }
export { editComplaintType }

export { editAccount }

export { accountStatuses }
export { newsStatuses }
export { weatherStatuses }
export { complaintStatuses }
export { complaintTypes }
export { policies }

export { accounts }
export { complaints }
export { comments }
export { news }
export { weathers }
export { notificationMessages }

export { editComplaint }
export { editComment }
export { editNews }
export { editWeather }
export { editNotificationMessage }

export { lastAction }