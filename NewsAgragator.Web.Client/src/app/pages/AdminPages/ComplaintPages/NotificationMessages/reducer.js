import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "NOTIFICATIONMESSAGE_LOADALL_SUCCESS":
            return {
                ...state,
                notificationMessages:
                {
                    loading: false,

                    value: action.data.map(tpr => ({
                        ...Object.fromEntries(Object.entries(tpr).map(x => ([
                            x[0],
                            {
                                value: x[1],
                            },
                        ]))),
                        user: {
                            ...Object.fromEntries(Object.entries(tpr.user).map(x => ([
                                x[0],
                                {
                                    value: x[1],
                                },
                            ]))),
                        },
                        administrator: {
                            ...Object.fromEntries(Object.entries(tpr.administrator).map(x => ([
                                x[0],
                                {
                                    value: x[1],
                                },
                            ]))),
                        },
                    })),
                },
            }

        case "NOTIFICATIONMESSAGE_REMOVE_START":
            return {
                ...state,
                notificationMessages:
                {
                    ...state.notificationMessages,
                    loading: true,
                }
            }

        case "NOTIFICATIONMESSAGE_LOADALL_START":
            return {
                ...state,
                notificationMessages:
                {
                    ...state.notificationMessages,
                    loading: true,
                }
            }

        default:
            return state
    }
}