import initialState from "../../../../../initialState";



export default (state = initialState.clientNewsFull, action) => {
    switch (action.type) {

        case "CLIENTNEWSFULL_SELECT":
            return {
                ...state,
                    [action.name]:
                    {
                        value: action.val,
                    },
            }

        case "CLIENTNEWSFULL_LOAD_PARAMETERS_START":
            return {
                ...state,
                    loadingParameters: true,
                    loadingData:false,
                    id: {
                        value: '',
                    },
                    title: {
                        value: '',
                    },
                    textHTML: {
                        value: '',
                    },
                    text: {
                        value: '',
                    },
                    sourceUrl: {
                        value: '',
                    },
                    date: {
                        value: '',
                    },
                    positiveRating: {
                        value: '',
                    },
                    comments:[],
            }

        case "CLIENTNEWSFULL_LOAD_PARAMETERS_SUCCESS":
            return {
                ...state,

                    loadingParameters: false,
                    loadingData:true,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {

                            value: x[1],
                        }
                    ])))

            }

        case "CLIENTNEWSFULL_LOAD_START":
            return {
                ...state,
                    loadingParameters:false,
                    loadingData: true,
            }

        case "CLIENTNEWSFULL_LOAD_SUCCESS":
            return {
                ...state,
                    loadingData: false,
                    loadingParameters:false,
                    ...state.comments,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {
                            value: x[1],
                        }
                    ]))),
            }

        default:
            return state
    }
}