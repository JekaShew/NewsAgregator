import initialState from "../../../../../initialState";



export default (state = initialState.editNotificationMessage, action) => {
    switch (action.type) {

        case "NOTIFICATIONMESSAGE_SELECT":
            return {
                ...state,
                // editNotificationMessage:
                // {
                //     ...state.editNotificationMessage,
                [action.name]:
                {
                    value: action.val,
                },
                // }
            }

        case "NOTIFICATIONMESSAGE_SELECT_PARAMETER":
            return {
                ...state,
                // editNotificationMessage:
                // {
                //     ...state.editNotificationMessage,
                [action.name]:
                {

                    id: action.val.value,
                    text: action.val.text,

                },
                // }
            }

        case "NOTIFICATIONMESSAGE_LOAD_PARAMETERS_START":
            return {
                ...state,
                // editNotificationMessage:
                // {
                loadingParameters: true,
                id: {
                    value: '',
                },
                title: {
                    value: '',
                },
                text: {
                    value: '',
                },
                user: {
                    value: {
                        id: '',
                        text: '',
                    },
                },

                administrator: {
                    value: {
                        id: '',
                        text: '',
                    },
                },

                accounts: {
                    value: {
                        id: '',
                        text: '',
                    },
                },
                // }
            }

        case 'NOTIFICATIONMESSAGE_LOAD_PARAMETERS_SUCCESS':
            return {
                ...state,
                // editNotificationMessage:
                // {
                //     ...state.editNotificationMessage,
                loadingParameters: false,
                ...Object.fromEntries(Object.entries(action.data).map(x => ([
                    x[0],
                    {

                        value: x[1],
                    }
                ])))
                // }

            }

        case "NOTIFICATIONMESSAGE_LOAD_START":
            return {
                ...state,
                // ...state.editNotificationMessage,
                // editNotificationMessage:
                // {
                loadingData: true,

                // }
            }

        case "NOTIFICATIONMESSAGE_LOAD_SUCCESS":
            return {
                ...state,
                // editNotificationMessage:
                // {
                //     ...state.editNotificationMessage,
                ...state.accounts,
                loadingData: false,
                ...Object.fromEntries(Object.entries(action.data).map(x => ([
                    x[0],
                    {
                        value: x[1],
                    }
                ]))),
                // }
            }

        default:
            return state
    }
}