//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity totalScoreEntity { get { return GetGroup(GameMatcher.TotalScore).GetSingleEntity(); } }
    public TotalScoreComponent totalScore { get { return totalScoreEntity.totalScore; } }
    public bool hasTotalScore { get { return totalScoreEntity != null; } }

    public GameEntity SetTotalScore(int newScore) {
        if (hasTotalScore) {
            throw new Entitas.EntitasException("Could not set TotalScore!\n" + this + " already has an entity with TotalScoreComponent!",
                "You should check if the context already has a totalScoreEntity before setting it or use context.ReplaceTotalScore().");
        }
        var entity = CreateEntity();
        entity.AddTotalScore(newScore);
        return entity;
    }

    public void ReplaceTotalScore(int newScore) {
        var entity = totalScoreEntity;
        if (entity == null) {
            entity = SetTotalScore(newScore);
        } else {
            entity.ReplaceTotalScore(newScore);
        }
    }

    public void RemoveTotalScore() {
        totalScoreEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TotalScoreComponent totalScore { get { return (TotalScoreComponent)GetComponent(GameComponentsLookup.TotalScore); } }
    public bool hasTotalScore { get { return HasComponent(GameComponentsLookup.TotalScore); } }

    public void AddTotalScore(int newScore) {
        var index = GameComponentsLookup.TotalScore;
        var component = (TotalScoreComponent)CreateComponent(index, typeof(TotalScoreComponent));
        component.Score = newScore;
        AddComponent(index, component);
    }

    public void ReplaceTotalScore(int newScore) {
        var index = GameComponentsLookup.TotalScore;
        var component = (TotalScoreComponent)CreateComponent(index, typeof(TotalScoreComponent));
        component.Score = newScore;
        ReplaceComponent(index, component);
    }

    public void RemoveTotalScore() {
        RemoveComponent(GameComponentsLookup.TotalScore);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherTotalScore;

    public static Entitas.IMatcher<GameEntity> TotalScore {
        get {
            if (_matcherTotalScore == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TotalScore);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTotalScore = matcher;
            }

            return _matcherTotalScore;
        }
    }
}