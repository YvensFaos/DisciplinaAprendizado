package twitter.api;

import twitter4j.Query;
import twitter4j.QueryResult;
import twitter4j.Status;
import twitter4j.Twitter;
import twitter4j.TwitterException;
import twitter4j.TwitterFactory;
import twitter4j.conf.ConfigurationBuilder;

public class MainClass {

	public static void main(String[] args) throws TwitterException {
		ConfigurationBuilder cb = new ConfigurationBuilder();
		cb.setDebugEnabled(true)
		 .setOAuthConsumerKey("5O15tlrfJlOnJs7X3qHcyyKUG")
		  .setOAuthConsumerSecret("JQrdi8dO89jv6rTvGOe9ze7KDTBCzOtmDV96GvZUUD04MGISjh")
		  .setOAuthAccessToken("2628406190-OyPKHBpwi7W4HOpLSs5Ub7E5JIKdD1AL6o3YsAw")
		  .setOAuthAccessTokenSecret("GEvVDz0nEQlvUXKnZMmYzyjB6aaQwEbYiIxcxLkTnHRnE");
		TwitterFactory tf = new TwitterFactory(cb.build());
		Twitter twitter = tf.getInstance();
		
		System.out.println(twitter);
		Query query = new Query("(dota2) AND (@ESLDota2)");
		query.count(50);
		QueryResult result = twitter.search(query);
		for (Status status : result.getTweets()) {
			System.out.println("@" + status.getUser().getScreenName() + ":"
					+ status.getText());
		}
	}
}
