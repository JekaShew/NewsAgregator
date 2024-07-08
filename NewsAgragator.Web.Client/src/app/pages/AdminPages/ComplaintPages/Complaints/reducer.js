import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "COMPLAINT_LOADALL_SUCCESS":
            return {
                ...state,
                complaints:
                {
                    loading: false,

                    value: action.data.map(tpr => ({
                        ...Object.fromEntries(Object.entries(tpr).map(x => ([
                            x[0],
                            {
                                value: x[1],
                            },
                        ]))),
                        comment: {
                            ...Object.fromEntries(Object.entries(tpr.comment).map(x => ([
                                x[0],
                                {
                                    value: x[1],
                                },
                            ]))),
                        },
                        news: {
                            ...Object.fromEntries(Object.entries(tpr.news).map(x => ([
                                x[0],
                                {
                                    value: x[1],
                                },
                            ]))),
                        },
                        complaintStatus: {
                            ...Object.fromEntries(Object.entries(tpr.complaintStatus).map(x => ([
                                x[0],
                                {
                                    value: x[1],
                                },
                            ]))),
                        },
                        complaintType: {
                            ...Object.fromEntries(Object.entries(tpr.complaintType).map(x => ([
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
                        user: {
                            ...Object.fromEntries(Object.entries(tpr.user).map(x => ([
                                x[0],
                                {
                                    value: x[1],
                                },
                            ]))),
                        },
                    })),
                },
            }

        case "COMPLAINT_REMOVE_START":
            return {
                ...state,
                complaints:
                {
                    ...state.complaints,
                    loading: true,
                }
            }

        case "COMPLAINT_LOADALL_START":
            return {
                ...state,
                complaints:
                {
                    ...state.complaints,
                    loading: true,
                }
            }

        default:
            return state
    }
}