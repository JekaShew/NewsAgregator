import initialState from "../../../../../initialState";



export default (state = initialState.editComment, action) => {
    switch (action.type) {

        case "COMMENT_SELECT":
            return {
                ...state,
                // editComment:
                // {
                //     ...state.editComment,
                    [action.name]:
                    {
                        value: action.val,
                    },
                // }
            }

        case "COMMENT_SELECT_PARAMETER":
            return {
                ...state,
                // editComment:
                // {
                //     ...state.editComment,
                    [action.name]:
                    {

                        id: action.val.value,
                        text: action.val.text,

                    },
                // }
            }

        case "COMMENT_LOAD_PARAMETERS_START":
            return {
                ...state,
                // editComment:
                // {
                    loadingParameters: true,
                    id: {
                        value: '',
                    },
                    text: {
                        value: '',
                    },
                    date: {
                        value: '',
                    },
                    news: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },

                    account: {
                        value: {
                            id: '',
                            text: '',
                        },
                    },

                    newses: {
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

        case 'COMMENT_LOAD_PARAMETERS_SUCCESS':
            return {
                ...state,
                // editComment:
                // {
                //     ...state.editComment,
                    loadingParameters: false,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {

                            value: x[1],
                        }
                    ])))
                // }

            }

        case "COMMENT_LOAD_START":
            return {
                ...state,
                // ...state.editComment,
                // editComment:
                // {
                    loadingData: true,

                // }
            }

        case "COMMENT_LOAD_SUCCESS":
            return {
                ...state,
                // editComment:
                // {
                //     ...state.editComment,
                    ...state.accounts,
                    ...state.newses,
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