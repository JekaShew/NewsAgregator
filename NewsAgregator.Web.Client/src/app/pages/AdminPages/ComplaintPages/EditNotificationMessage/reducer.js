import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "NOTIFICATIONMESSAGE_SELECT":
            return {
                ...state,
                editNotificationMessage:
                {
                    ...state.editNotificationMessage,
                    [action.name]:
                    {
                        value: action.val,
                    },
                }
            }

        case 'NOTIFICATIONMESSAGE_LOAD_PARAMETERS_SUCCESS':
            return {
                ...state,
                editNotificationMessage:
                {
                    ...state.editNotificationMessage,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {
                            value: state.editNotificationMessage[x[0]].value,
                            options: x[1],
                        }
                    ])))
                }

            }

        case "NOTIFICATIONMESSAGE_LOAD_SUCCESS":
            return {
                ...state,
                editNotificationMessage:
                {
                    ...state.editNotificationMessage,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {
                            value: x[1],
                        }
                    ]))),
                }
            }

        default:
            return state
    }
}