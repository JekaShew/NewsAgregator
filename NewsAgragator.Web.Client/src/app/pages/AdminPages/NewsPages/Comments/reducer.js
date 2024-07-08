import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "COMMENT_LOADALL_SUCCESS":
            return {
                ...state,
                comments:
                {
                    loading: false,

                    value: action.data.map(tpr => ({
                        ...Object.fromEntries(Object.entries(tpr).map(x => ([
                            x[0],
                            {
                                value: x[1],
                            },
                        ]))),
                        account: {
                            ...Object.fromEntries(Object.entries(tpr.account).map(x => ([
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
                    })),
                },
            }

        case "COMMENT_REMOVE_START":
            return {
                ...state,
                comments:
                {
                    ...state.comments,
                    loading: true,
                }
            }

        case "COMMENT_LOADALL_START":
            return {
                ...state,
                comments:
                {
                    ...state.comments,
                    loading: true,
                }
            }

        default:
            return state
    }
}