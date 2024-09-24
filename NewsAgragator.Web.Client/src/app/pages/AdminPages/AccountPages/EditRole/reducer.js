import { text } from "@fortawesome/fontawesome-svg-core";
import initialState from "../../../../../initialState";
import { policies } from "../../../../../reducers";



export default (state = initialState.editRole, action) => {
    switch (action.type) {

        case "ROLE_SELECT":
            return {
                ...state,

                    [action.name]:
                    {
                        value: action.val,
                    },
            }



            case "ROLE_LOAD_PARAMETERS_START":
                return {
                    ...state,
                    // editNews:
                    // {
                        loadingParameters: true,
                        id: {
                            value: '',
                        },
                        title: {
                            value: '',
                        },
                        description: {
                            value: '',
                        },    
                        policies: {
                            value: [],
                            options: [],
                        },
                }
    
            case "ROLE_LOAD_PARAMETERS_SUCCESS":
                return {
                    ...state,    
                    loadingParameters: false,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {

                            value: state[x[0]].value,
                            options:x[1]
                        }
                    ])))
                }
    
            case "ROLE_LOAD_START":
                return {
                    ...state,
                    loadingData: true,
                }
    
            case "ROLE_LOAD_SUCCESS":
                return {
                    ...state,
                    ...state.policies,
                    loadingData: false,
                    ...Object.fromEntries(Object.entries(action.data).map(x => ([
                        x[0],
                        {
                            value: x[1],
                        }
                    ]))),
                    policies:{
                        value: action.data.policiesIDs,
                        options: state.policies.options,
                    }
                }


        case 'ROLE_ADDLIST':
            return {
                ...state,
                    [action.name]:
                    {
                        ...state[action.name],
                        value:  state[action.name].value.concat([action.val]),                      
                    }
            }

        case 'ROLE_REMOVELIST':
            return {
                ...state,
                [action.name]:
                {
                    ...state[action.name],
                    value: state[action.name].value.filter(x => x != action.val),
                }
            }

        default:
            return state
    }
}